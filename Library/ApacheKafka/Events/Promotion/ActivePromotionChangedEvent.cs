using System;
using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Promotion
{
    public class ActivePromotionListChangedEvent : IPubSubEvent
    {
        public Guid Version {get; set; }
        public PubSubEventType Topic { get; } = PubSubEventType.ActivePromotionListChanged;
        public bool ConsumeSynchronously => false;
        public string AllActivePromotions { get; set; } = null;
    }
}
