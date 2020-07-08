using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Promotion
{
    public class PromotionStartedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.PromotionStarted;

        public bool ConsumeSynchronously { get; } = true;
        public string CampaignId { get; set; }
        public string PromotionId { get; set; }
        public PromotionEventInfo PromotionEventInfo { get; set; }


    }
}
