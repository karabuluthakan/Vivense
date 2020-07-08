using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.SupplierApplication
{
    public class SupplierApplicationCreated : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.SupplierApplicationCreated;

        public bool ConsumeSynchronously => true;

        public SupplierApplicationEventModel SupplierApplication { get; set; }
    }
}
