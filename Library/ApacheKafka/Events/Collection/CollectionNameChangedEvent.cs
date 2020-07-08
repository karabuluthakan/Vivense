using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Collection
{
    public class CollectionNameChangedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.CollectionNameChanged;
        public bool ConsumeSynchronously { get; } = true;
        public string CollectionId { get; set; }
        public string NewName { get; set; }
    }
}
