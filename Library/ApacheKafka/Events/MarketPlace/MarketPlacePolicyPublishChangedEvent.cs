using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.MarketPlace
{
    public class MarketPlacePolicyPublishChangedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.MarketPlacePolicyPublishChanged;
        public bool ConsumeSynchronously { get; } = true;
        public string MarketPlacePolicyId { get; set; }
        public bool Published { get; set; }
    }
}