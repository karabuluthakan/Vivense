using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Entity
{
    public class EntityCreateEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.EntityCreated;
        public bool ConsumeSynchronously { get; } = true;
        
        public EntityEventModel Entity { get; set; }
    }
}