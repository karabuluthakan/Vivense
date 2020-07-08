using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Attribute
{
    public class AttributeUpdated : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.AttributeUpdated;
        public bool ConsumeSynchronously => true;
        public AttributeModel Attribute { get; set; }
    }
}
