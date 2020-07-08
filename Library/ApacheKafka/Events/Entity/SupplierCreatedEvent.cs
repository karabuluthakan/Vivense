using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Entity
{
    public class SupplierCreatedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.SupplierCreated;
        public bool ConsumeSynchronously => true;
        public SupplierEntityModel SupplierModel { get; set; }
    }
}
