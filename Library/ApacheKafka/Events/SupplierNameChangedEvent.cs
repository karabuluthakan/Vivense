using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events
{
    public class SupplierNameChangedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.SupplierNameChanged;
        public bool ConsumeSynchronously { get; } = true;
        public string SupplierId { get; set; }
        public string NewName { get; set; }
    }
}
