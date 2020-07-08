using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;
using Library.Models.Enums;

namespace Library.ApacheKafka.Events.Entity
{
    public class EntityDeleteEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.EntityDeleted;
        public bool ConsumeSynchronously { get; } = true;
        public string EntityId { get; set; }
        public EntityType EntityType { get; set; }
    }
}