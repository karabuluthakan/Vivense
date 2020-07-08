using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Category
{
    public class CategoryUpdatedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.CategoryUpdated;
        public bool ConsumeSynchronously => false;
        public CategoryEventInfo OldCategory { get; set; }
        public CategoryEventInfo NewCategory { get; set; }
    }
}
