using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;
using Library.Models.Lookups;

namespace Library.ApacheKafka.Events.Category
{
    public class CategoryTitleChangedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.CategoryTitleChanged;
        public bool ConsumeSynchronously { get; } = true;
        public string CategoryId { get; set; }
        public MultiLanguage NewTitle { get; set; }
    }
}
