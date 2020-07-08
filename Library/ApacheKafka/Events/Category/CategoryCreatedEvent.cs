using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Category
{
    public class CategoryCreatedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.CategoryCreated;

        public bool ConsumeSynchronously => false;
        public CategoryEventInfo NewCategory { get; set; }
    }
}
