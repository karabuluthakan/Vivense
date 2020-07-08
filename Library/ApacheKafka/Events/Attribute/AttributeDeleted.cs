using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Attribute
{
    public class AttributeDeleted : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.AttributeDeleted;
        public bool ConsumeSynchronously => true;
        public string AttributeId { get; set; }
    }
}
