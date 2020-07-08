using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;
using Library.Models.Lookups;

namespace Library.ApacheKafka.Events
{
    public class AttributeTitleChangedEvent: IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.AttributeTitleChanged;
        public bool ConsumeSynchronously { get; } = true;
        public string AttributeId { get; set; }
        public MultiLanguage NewTitle { get; set; }
    }
}
