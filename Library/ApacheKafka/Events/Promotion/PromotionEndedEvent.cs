using System.Collections.Generic;
using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Promotion
{
    public class PromotionEndedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.PromotionEnded;
        public bool ConsumeSynchronously { get; } = true;
        public string[] CampaignIds { get; set; } = new string[0];
        public List<PromotionQualifierInfo> DiscountProducts { get; set; }
        public string PromotionId { get; set; }
    }
}
