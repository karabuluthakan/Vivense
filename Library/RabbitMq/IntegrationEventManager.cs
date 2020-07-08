using System.Threading.Tasks;
using Library.RabbitMq.Abstract;
using Library.RabbitMq.Events;

namespace Library.RabbitMq
{
    public class IntegrationEventManager : IIntegrationEventService
    {
        private readonly IEventBus eventBus;

        public IntegrationEventManager(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public Task PublishThroughEventBusAsync(IntegrationEvent @event)
        {
            eventBus.Publish(@event);
            return Task.CompletedTask;
        }
    }
}