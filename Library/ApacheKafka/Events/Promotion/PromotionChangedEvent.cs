using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Promotion
{
    public class PromotionChangedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.PromotionChanged;
        public bool ConsumeSynchronously { get; } = true;
        
        public string PromotionId {get; set; }
    }
}