using System.Collections.Generic;
using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Promotion
{
    public class PromotionCalculateFinalEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.PromotionCalculateFinal;

        public bool ConsumeSynchronously { get; } = true;

        public List<PromotionCalculateEventInfo> PromotionForProducts { get; set; }
    }
}
