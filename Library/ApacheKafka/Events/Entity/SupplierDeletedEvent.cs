using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Entity
{
    public class SupplierDeletedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.SupplierRemoved;

        public bool ConsumeSynchronously => true;

        public string SupplierId { get; set; }
    }
}
