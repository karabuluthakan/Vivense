using System.Threading.Tasks;

namespace Library.RabbitMq.Abstract
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}