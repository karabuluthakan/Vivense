using System.Collections.Generic;
using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Product.Events
{
    public class ProductFilesProcessResultEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.ProductFilesResult;

        public bool ConsumeSynchronously => true;
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public string TransactionId { get; set; }
        public string ReceiverEmail { get; set; }

    }
}
