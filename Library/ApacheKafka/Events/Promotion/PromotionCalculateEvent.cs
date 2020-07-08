using System.Collections.Generic;
using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Promotion
{
   public class PromotionCalculateEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.PromotionCalculate;

        public bool ConsumeSynchronously { get; } = true;
        public string CampaignId { get; set; }
        public string PromotionId { get; set; }
        public List<PromotionCalculateEventInfo> PromotionForProducts { get; set; }
    }
}
