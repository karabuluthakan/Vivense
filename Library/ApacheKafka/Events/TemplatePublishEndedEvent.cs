using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events
{
    public class TemplatePublishEndedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.TemplatePublishEnded;

        public bool ConsumeSynchronously { get; } = true;

        public string TemplateId { get; set; }
    }
}
