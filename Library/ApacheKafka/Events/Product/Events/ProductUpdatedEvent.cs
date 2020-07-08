using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Events.Product.Infos;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Product.Events
{
    public class ProductUpdatedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.ProductUpdated;
        public bool ConsumeSynchronously { get; } = true;

        public ProductInfo ProductBeforeUpdate { get; set; } 
        public ProductInfo ProductAfterUpdate { get; set; } 
    }
}