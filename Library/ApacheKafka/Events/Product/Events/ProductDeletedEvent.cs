using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Events.Product.Infos;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Product.Events
{
    public class ProductDeletedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.ProductDeleted;
        public bool ConsumeSynchronously { get; } = true;

        public ProductInfo DeletedProduct { get; set; } 
    }
}