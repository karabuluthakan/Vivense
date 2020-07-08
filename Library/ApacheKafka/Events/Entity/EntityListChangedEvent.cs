using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;
using Library.CrossCuttingConcerns.Authorization.Models;

namespace Library.ApacheKafka.Events.Entity
{
    public class EntityListChangedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.EntityListChanged;
        public bool ConsumeSynchronously => false;
        public EntityList List { get; } = new EntityList();

        public EntityListChangedEvent(EntityList list)
        {
            this.List = list;
        }

        public EntityListChangedEvent()
        {
        }
    }
}