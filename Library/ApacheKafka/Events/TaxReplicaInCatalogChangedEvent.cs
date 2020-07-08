using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;
using Library.Models;

namespace Library.ApacheKafka.Events
{
    public class TaxReplicaInCatalogChangedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.TaxReplicaInCatalogChanged;
        public bool ConsumeSynchronously { get; } = true;
        public TaxEventInfo OldTax { get; set; }
        public TaxEventInfo NewTax { get; set; }

    }
}
