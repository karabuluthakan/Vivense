using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Entity
{
    public class SupplierUpdatedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.SupplierChanged;
        public bool ConsumeSynchronously => true;
        public SupplierEntityModel SupplierModel { get; set; }
    }
}
