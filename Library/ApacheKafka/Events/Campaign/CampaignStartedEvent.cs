using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Campaign
{
    public class CampaignStartedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.CampaignStarted;

        public bool ConsumeSynchronously { get; } = true;

        public string CampaignId { get; set; }
    }
}
