using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Events.Product.Infos;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Product.Events
{
    public class ProductCreatedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.ProductCreated;
        public bool ConsumeSynchronously { get; } = true;

        public ProductInfo NewProduct { get; set; } 
    }
}