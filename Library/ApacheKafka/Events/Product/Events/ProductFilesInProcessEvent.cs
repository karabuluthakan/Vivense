using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Product.Events
{
    public class ProductFilesInProcessEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.ProductFilesInProcess;
        public bool ConsumeSynchronously { get; } = true;
        public string TransactionId { get; set; }
        
        public string ReceiverEmail { get; set; }
    }
}