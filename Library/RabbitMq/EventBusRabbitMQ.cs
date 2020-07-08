using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Autofac;
using Library.Extensions;
using Library.RabbitMq.Abstract;
using Library.RabbitMq.Events;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Library.RabbitMq
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        private const string BROKER_NAME = "canavar_ng_event_bus";

        private readonly IRabbitMQPersistentConnection persistentConnection;
        private readonly ILogger<EventBusRabbitMQ> logger;
        private readonly IEventBusSubscriptionsManager subsManager;
        private readonly ILifetimeScope autofac;
        private const string AUTOFAC_SCOPE_NAME = "canavar_ng_event_bus";
        private readonly int retryCount;

        private IModel consumerChannel;
        private string queueName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="persistentConnection"></param>
        /// <param name="logger"></param> 
        /// <param name="subsManager"></param>
        /// <param name="autofac"></param>
        /// <param name="queueName"></param>
        /// <param name="retryCount"></param>
        public EventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection, ILogger<EventBusRabbitMQ> logger,
            IEventBusSubscriptionsManager subsManager, ILifetimeScope autofac, string queueName = null, int retryCount = 5)
        {
            this.persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.autofac = autofac;
            this.subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            this.queueName = queueName;
            consumerChannel = CreateConsumerChannel();
            this.retryCount = retryCount;
            this.subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventName"></param>
        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            using var channel = persistentConnection.CreateModel();
            channel.QueueUnbind(queue: queueName, exchange: BROKER_NAME, routingKey: eventName);

            if (subsManager.IsEmpty)
            {
                queueName = string.Empty;
                consumerChannel.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="event"></param>
        public void Publish(IntegrationEvent @event)
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (ex, time) =>
                    {
                        logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id,
                            $"{time.TotalSeconds:n1}", ex.Message);
                    });

            using var channel = persistentConnection.CreateModel();
            var eventName = @event.GetType()
                .Name;

            channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");

            var message = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {
                var properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2; // persistent

                channel.BasicPublish(exchange: BROKER_NAME,
                    routingKey: eventName,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TH"></typeparam>
        /// <param name="eventName"></param>
        public void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            logger.LogInformation($"Subscribing to dynamic event {eventName} with {typeof(TH).GetGenericTypeName()}");
            DoInternalSubscription(eventName);
            subsManager.AddDynamicSubscription<TH>(eventName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TH"></typeparam>
        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = subsManager.GetEventKey<T>();
            DoInternalSubscription(eventName);

            logger.LogInformation($"Subscribing to event {eventName} with {typeof(TH).GetGenericTypeName()}");

            subsManager.AddSubscription<T, TH>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventName"></param>
        private void DoInternalSubscription(string eventName)
        {
            var containsKey = subsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                if (!persistentConnection.IsConnected)
                {
                    persistentConnection.TryConnect();
                }

                using var channel = persistentConnection.CreateModel();
                channel.QueueBind(queue: queueName, exchange: BROKER_NAME, routingKey: eventName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TH"></typeparam>
        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = subsManager.GetEventKey<T>();
            logger.LogInformation($"Unsubscribing from event {eventName}");
            subsManager.RemoveSubscription<T, TH>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TH"></typeparam>
        /// <param name="eventName"></param>
        public void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            subsManager.RemoveDynamicSubscription<TH>(eventName);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            consumerChannel?.Dispose();
            subsManager.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IModel CreateConsumerChannel()
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            var channel = persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");

            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var eventName = ea.RoutingKey;
                var message = JsonSerializer.Serialize(ea.Body);

                await ProcessEvent(eventName, message);

                channel.BasicAck(ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

            channel.CallbackException += (sender, ea) =>
            {
                consumerChannel.Dispose();
                consumerChannel = CreateConsumerChannel();
            };

            return channel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task ProcessEvent(string eventName, string message)
        {
            if (subsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    var subscriptions = subsManager.GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        if (subscription.IsDynamic)
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType) as IDynamicIntegrationEventHandler;
                            if (handler == null)
                            {
                                continue;
                            }

                            dynamic eventData = JObject.Parse(message);
                            await handler.Handle(eventData);
                        } else
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType);
                            if (handler == null)
                            {
                                continue;
                            }

                            var eventType = subsManager.GetEventTypeByName(eventName);
                            var integrationEvent = JsonSerializer.Serialize(message, eventType);
                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                            await (Task) concreteType.GetMethod("Handle").Invoke(handler, new object[] {integrationEvent});
                        }
                    }
                }
            }
        }
    }
}