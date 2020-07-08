using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Collection
{
    public class CollectionStatusChangedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.CollectionStatusChanged;
        public bool ConsumeSynchronously { get; } = true;
        public string CollectionId { get; set; }
        public bool Published { get; set; }
    }
}