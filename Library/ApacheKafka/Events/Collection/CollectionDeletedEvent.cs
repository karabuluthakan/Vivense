using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Collection
{
    public class CollectionDeletedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.CollectionDeleted;
        public bool ConsumeSynchronously { get; } = true;
        public string CollectionId { get; set; }
    }
}
