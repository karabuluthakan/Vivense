using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Campaign
{
    public class CampaignChangedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.CampaignChanged;
        public bool ConsumeSynchronously { get; } = true;
        public string CampaignId { get; set; }
    }
}