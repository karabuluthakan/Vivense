using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Collection
{
    public class CollectionUpdated : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.CollectionUpdated;
        public bool ConsumeSynchronously => true;
        public CollectionModel Collection { get; set; }
        public CollectionModel OldCollection { get; set; }
    }
}
