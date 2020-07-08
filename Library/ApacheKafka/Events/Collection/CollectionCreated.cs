using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Collection
{
    public class CollectionCreated : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.CollectionCreated;
        public bool ConsumeSynchronously => true;
        public CollectionModel Collection { get; set; }
    }
}
