using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;
using Library.Models.Lookups;

namespace Library.ApacheKafka.Events
{
    public class TemplatePublishedEventNew : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.TemplatePublishedNew;
        public bool ConsumeSynchronously { get; } = true;
        public string TemplateId { get; set; }
        public LookupIdName PublishedBy { get; set; }
    }
}
