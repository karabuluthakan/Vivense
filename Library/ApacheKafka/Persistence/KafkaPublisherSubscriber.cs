using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Library.ApacheKafka.Abstract;
using Library.CrossCuttingConcerns.Logging;
using Library.Utilities.AppSettings;
using Microsoft.Extensions.Options;

namespace Library.ApacheKafka.Persistence
{
    public class KafkaPublisherSubscriber : IPublisherSubscriber, IDisposable
    {
        private readonly IProducer<Null, string> producer;
        protected readonly List<KafkaExtendedConsumer> extendedConsumers = new List<KafkaExtendedConsumer>();
        private readonly string bootstrapServers;
        private readonly string topicSuffix = null;
        private readonly ILoggerService logger;

        public string TopicSuffix => topicSuffix;

        private readonly object _locker = new object();

        private readonly string serviceInstanceSpecificGroupId;

        public KafkaPublisherSubscriber(IOptions<KafkaSettings> settingOptions, ILoggerService logger)
        {
            bootstrapServers = settingOptions.Value.BootstrapServers;
            topicSuffix = settingOptions.Value.TopicSuffix;
            this.logger = logger;
            var config = new ProducerConfig {BootstrapServers = bootstrapServers, RequestTimeoutMs = 2000, MessageTimeoutMs = 10000};
            producer = new ProducerBuilder<Null, string>(config).Build();
            serviceInstanceSpecificGroupId = Guid.NewGuid().ToString();
        }

        public virtual void Publish(IPubSubEvent evt)
        {
            var topic = topicSuffix == null ? evt.Topic.ToString() : evt.Topic.ToString() + topicSuffix;
            var msg = JsonSerializer.Serialize(evt);
            producer.ProduceAsync(topic, new Message<Null, string> {Value = msg});
        }

        public virtual async Task<DeliveryResult<Null, string>> PublishAsync(IPubSubEvent @event)
        {
            var topic = topicSuffix == null ? @event.Topic.ToString() : @event.Topic.ToString() + topicSuffix;
            var message = JsonSerializer.Serialize(@event);
            return await producer.ProduceAsync(topic, new Message<Null, string> {Value = message});
        }

        /// <summary>
        /// Try to subscribe in one batch where possible.
        /// </summary>
        /// <param name="subscriptionConfigs"></param>
        public virtual void Subscribe(List<ISubscriptionConfig> subscriptionConfigs)
        {
            logger.LogCritical($"KafkaDebug kd1 "
                               + "Subscribe configs: "
                               + JsonSerializer.Serialize(subscriptionConfigs));

            if (topicSuffix != null)
            {
                AddSuffixToTopics(subscriptionConfigs);
            }

            SubscribeAsyncSubscriptions(subscriptionConfigs);
            SubscribeSyncSubscriptions(subscriptionConfigs);
        }

        private void AddSuffixToTopics(List<ISubscriptionConfig> subscriptionConfigs)
        {
            foreach (var subscriptionConfig in subscriptionConfigs)
            {
                subscriptionConfig.AddTopicSuffix(topicSuffix);
            }
        }

        private void SubscribeAsyncSubscriptions(List<ISubscriptionConfig> subscriptionConfigs)
        {
            var differentSubscriptionGroups = subscriptionConfigs
                .Where(s => !s.ConsumeEventsSynchronously)
                .GroupBy(s => new {s.GroupId, s.StartOfProcessing})
                .ToList();

            foreach (var subscriptionGroup in differentSubscriptionGroups)
            {
                KafkaExtendedConsumer existingExtendedConsumerWithSameConfig;
                lock (_locker)
                {
                    existingExtendedConsumerWithSameConfig = extendedConsumers
                        .FirstOrDefault(c =>
                            c.ConsumerConfig.GroupId == subscriptionGroup.Key.GroupId
                            && c.ConsumerConfig.AutoOffsetReset == subscriptionGroup.Key.StartOfProcessing);
                }

                var subscriptions = subscriptionGroup.Select(c => new KafkaSubscription()
                {
                    SubscriptionId = c.Id,
                    Topic = c.Topic,
                    Handler = c.OnEventReceived
                }).ToList();

                //If consumer with same config already exists, cancel consume and let the consumer pick up the new subscriptions
                if (existingExtendedConsumerWithSameConfig != null)
                {
                    subscriptions.AddRange(existingExtendedConsumerWithSameConfig.Subscriptions);

                    lock (_locker)
                    {
                        existingExtendedConsumerWithSameConfig.Subscriptions = subscriptions;
                    }

                    existingExtendedConsumerWithSameConfig.CancellationTokenSource.Cancel();
                    return;
                }

                //Do not allow subscription configs with the same id multiple times
                subscriptions = subscriptions.GroupBy(s => s.SubscriptionId).Select(s => s.First()).ToList();

                var conf = new ConsumerConfig
                {
                    GroupId = subscriptionGroup.Key.GroupId
                              ?? serviceInstanceSpecificGroupId, //if group id is left null, it should default to the service instance specific group id
                    BootstrapServers = bootstrapServers,
                    AutoOffsetReset = subscriptionGroup.Key.StartOfProcessing,
                    //FetchWaitMaxMs = 100000, //Interval with witch consumer will refresh fetch requests. Default is 100.
                    //SocketTimeoutMs = 110000 //Should be set to a value greater than FetchWaitMaxMs in order to prevent errors
                };

                Task.Factory.StartNew(() => CreateConsumer(conf, subscriptions), TaskCreationOptions.LongRunning);
            }
        }

        private void SubscribeSyncSubscriptions(List<ISubscriptionConfig> subscriptionConfigs)
        {
            //Create different consumer for each {groupID, StartOfProcessing, Topic } group 
            var differentSubscriptionGroups = subscriptionConfigs
                .Where(s => s.ConsumeEventsSynchronously)
                .GroupBy(s => new {s.GroupId, s.StartOfProcessing, s.Topic})
                .ToList();

            foreach (var subscriptionGroup in differentSubscriptionGroups)
            {
                KafkaExtendedConsumer existingExtendedConsumerWithSameConfig;
                lock (_locker)
                {
                    existingExtendedConsumerWithSameConfig = extendedConsumers
                        .FirstOrDefault(c =>
                            c.ConsumerConfig.GroupId == subscriptionGroup.Key.GroupId
                            && c.ConsumerConfig.AutoOffsetReset == subscriptionGroup.Key.StartOfProcessing
                            && c.Subscriptions.Any(s => s.Topic == subscriptionGroup.Key.Topic));
                }

                var subscriptions = subscriptionGroup.Select(c => new KafkaSubscription()
                {
                    SubscriptionId = c.Id,
                    Topic = c.Topic,
                    Handler = c.OnEventReceived
                }).ToList();

                //If consumer with same config already exists, cancel consume and let the consumer pick up the new subscriptions
                if (existingExtendedConsumerWithSameConfig != null)
                {
                    subscriptions.AddRange(existingExtendedConsumerWithSameConfig.Subscriptions);

                    lock (_locker)
                    {
                        existingExtendedConsumerWithSameConfig.Subscriptions = subscriptions;
                    }

                    existingExtendedConsumerWithSameConfig.CancellationTokenSource.Cancel();
                    return;
                }

                //Do not allow subscription configs with the same id multiple times
                subscriptions = subscriptions.GroupBy(s => s.SubscriptionId).Select(s => s.First()).ToList();

                var conf = new ConsumerConfig
                {
                    GroupId = subscriptionGroup.Key.GroupId
                              ?? serviceInstanceSpecificGroupId, //if group id is left null, it should default to the service instance specific group id
                    BootstrapServers = bootstrapServers,
                    AutoOffsetReset = subscriptionGroup.Key.StartOfProcessing,
                    //FetchWaitMaxMs = 100000 //Interval with witch consumer will refresh fetch requests. Default is 100.
                };

                Task.Factory.StartNew(() => CreateConsumer(conf, subscriptions, true), TaskCreationOptions.LongRunning);
            }
        }

        public virtual void Unsubscribe(string subscriptionConfigId)
        {
            KafkaExtendedConsumer extendedConsumerWithSubscriptionConfigId;
            lock (_locker)
            {
                extendedConsumerWithSubscriptionConfigId = extendedConsumers
                    .FirstOrDefault(c => c.Subscriptions.Any(s => s.SubscriptionId == subscriptionConfigId));
            }

            if (extendedConsumerWithSubscriptionConfigId == null)
            {
                return;
            }

            var subscriptionToCancel =
                extendedConsumerWithSubscriptionConfigId.Subscriptions.Single(s =>
                    s.SubscriptionId == subscriptionConfigId);

            logger.LogCritical($"KafkaDebug kd2 Unsubscribe. Topic: {subscriptionToCancel.Topic}" );

            lock (_locker)
            {
                extendedConsumerWithSubscriptionConfigId.Subscriptions.Remove(subscriptionToCancel);
            }

            extendedConsumerWithSubscriptionConfigId.CancellationTokenSource.Cancel();
        }


        private void CreateConsumer(ConsumerConfig consumerConfig, List<KafkaSubscription> subscriptions,
            bool consumeEventsSynchronously = false)
        {
            try
            {
                logger.LogCritical("KafkaDebug kd16 ");
                var consumerBuilder = new ConsumerBuilder<Ignore, string>(consumerConfig)
                    .SetErrorHandler((consumer, error) =>
                    {
                        logger.LogCritical($"KafkaDebug kd17 Kafka internal error during consume: {error.Reason}");
                        var extendedConsumerWithError = extendedConsumers
                            .FirstOrDefault(c => c.Consumer.Name == consumer.Name);
                        extendedConsumerWithError?.CancellationTokenSource.Cancel();
                    });
                using (var consumer = consumerBuilder.Build())
                {
                    logger.LogCritical($"KafkaDebug kd3 New consumer created. Consumer config: {JsonSerializer.Serialize(consumerConfig)}");

                    var cts = new CancellationTokenSource();

                    var extendedConsumer = new KafkaExtendedConsumer()
                    {
                        Consumer = consumer,
                        ConsumerConfig = consumerConfig,
                        Subscriptions = subscriptions,
                        CancellationTokenSource = cts
                    };

                    lock (_locker)
                    {
                        extendedConsumers.Add(extendedConsumer);
                    }

                    var topics = subscriptions.Select(s => s.Topic).ToList();

                    try
                    {
                        logger.LogCritical($"KafkaDebug kd12 ");
                        consumer.Subscribe(topics.Distinct());
                        logger.LogCritical($"KafkaDebug kd4 Consumer subscribe: {JsonSerializer.Serialize(topics.Distinct())}");
                    } catch (Exception ex)
                    {
                        logger.LogCritical($"KafkaDebug kd5 {ex}");
                        Task.Factory.StartNew(() =>
                            {
                                Thread.Sleep(60000);
                                CreateConsumer(consumerConfig, subscriptions, consumeEventsSynchronously);
                            },
                            TaskCreationOptions.LongRunning);
                        return;
                    }


                    while (true)
                    {
                        try
                        {
                            logger.LogCritical("KafkaDebug kd13 ");
                            var consumeResult = consumer.Consume(cts.Token);
                            logger.LogCritical("KafkaDebug kd6 Consumer consume");
                            var eventHandlers = new List<Func<string, Task>>();

                            lock (_locker)
                            {
                                eventHandlers = extendedConsumer.Subscriptions
                                    .Where(s => s.Topic == consumeResult.Topic)
                                    .Select(s => s.Handler)
                                    .ToList();
                            }

                            if (!consumeEventsSynchronously)
                            {
                                Task.Factory.StartNew(async () =>
                                {
                                    foreach (var eventHandler in eventHandlers)
                                    {
                                        try
                                        {
                                            logger.LogCritical($"KafkaDebug !consumeEventsSynchronously {consumeResult.Value}");
                                            await eventHandler(consumeResult.Value);
                                        } catch (Exception ex)
                                        {
                                            logger.LogCritical($"KafkaDebug {ex}");
                                        }
                                    }
                                }, TaskCreationOptions.LongRunning);
                            } else
                            {
                                foreach (var eventHandler in eventHandlers)
                                {
                                    try
                                    {
                                        logger.LogCritical($"KafkaDebug consumeEventsSynchronously {consumeResult.Value}");
                                        eventHandler(consumeResult.Value).Wait(cts.Token);
                                    } catch (Exception ex)
                                    {
                                        logger.LogCritical($"KafkaDebug {ex}");
                                    }
                                }
                            }
                        } catch (Exception ex)
                        {
                            logger.LogCritical($"KafkaDebug kd7 {ex}");
                            if (!(ex is OperationCanceledException))
                            {
                                logger.LogCritical($"KafkaDebug kd8 {ex}");
                            }

                            lock (_locker)
                            {
                                topics = extendedConsumer.Subscriptions.Select(s => s.Topic).ToList();
                            }

                            if (topics.Count == 0)
                            {
                                // Ensure the consumer leaves the group cleanly and final offsets are committed.
                                try
                                {
                                    logger.LogCritical($"KafkaDebug " + " Consumer close");
                                    consumer.Close();
                                    logger.LogCritical($"KafkaDebug kd14 ");
                                } catch (Exception exClose)
                                {
                                    logger.LogCritical($"KafkaDebug {ex}");
                                }

                                lock (_locker)
                                {
                                    extendedConsumers.Remove(extendedConsumer);
                                }

                                return;
                            }

                            cts = new CancellationTokenSource();
                            lock (_locker)
                            {
                                extendedConsumer.CancellationTokenSource = cts;
                            }

                            try
                            {
                                logger.LogCritical($"KafkaDebug kd9 "
                                                   + " Resubscribe after consume returns. Topics:"
                                                   + string.Join(",", topics.Distinct()));

                                consumer.Subscribe(topics.Distinct());
                                logger.LogCritical("KafkaDebug kd15 ");
                            } catch (Exception ex2)
                            {
                                logger.LogCritical($"KafkaDebug kd10 {ex2}");

                                Task.Factory.StartNew(() =>
                                    {
                                        Thread.Sleep(60000);
                                        CreateConsumer(consumerConfig, subscriptions, consumeEventsSynchronously);
                                    },
                                    TaskCreationOptions.LongRunning);
                                return;
                            }
                        }
                    }
                }
            } catch (Exception ex)
            {
                logger.LogCritical($"KafkaDebug kd11 CreateConsumer failed without recovery. {ex}");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            producer.Dispose();
            var consumers = extendedConsumers.Select(ec => ec.Consumer).ToList();
            foreach (var consumer in consumers)
            {
                consumer.Close();
            }
        }
    }
}