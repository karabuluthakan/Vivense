using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Category
{
    
    public class CategoryStatusChangedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.CategoryStatusChanged;
        public bool ConsumeSynchronously { get; } = true; 
        public string CategoryId { get; set; }
        public bool Published { get; set; } 
    }
}