using System.Threading.Tasks;
using Library.RabbitMq.Events;

namespace Library.RabbitMq.Abstract
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IIntegrationEventHandler
    {
    }
}