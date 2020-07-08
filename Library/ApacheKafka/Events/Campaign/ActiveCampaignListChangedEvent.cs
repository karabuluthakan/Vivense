using System;
using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Campaign
{
    public class ActiveCampaignListChangedEvent : IPubSubEvent
    {
        public Guid Version {get; set; }
        public PubSubEventType Topic { get; } = PubSubEventType.ActiveCampaignChanged;
        public bool ConsumeSynchronously { get; } = false;
        public string AllActiveCampaigns { get; set; } = null;
    }
}