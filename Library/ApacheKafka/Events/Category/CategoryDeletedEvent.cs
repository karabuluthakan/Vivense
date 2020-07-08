using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Category
{
    public class CategoryDeletedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.CategoryDeleted;

        public bool ConsumeSynchronously => false;

        public CategoryEventInfo DeletedCategory { get; set; }
    }
}
