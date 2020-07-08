using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Campaign
{
   public class CampaignEndedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.CampaignEnded;

        public bool ConsumeSynchronously { get; } = true;

        public string CampaignId { get; set; }
    }
}
