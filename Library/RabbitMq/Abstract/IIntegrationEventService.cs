using System.Threading.Tasks;
using Library.RabbitMq.Events;

namespace Library.RabbitMq.Abstract
{
    public interface IIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent @event);
    }
}