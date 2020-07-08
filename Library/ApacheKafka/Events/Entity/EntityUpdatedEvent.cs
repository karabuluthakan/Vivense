using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Entity
{
    public class EntityUpdatedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.EntityUpdated;
        public bool ConsumeSynchronously { get; } = true;

        public EntityEventModel Entity { get; set; }
    }
}