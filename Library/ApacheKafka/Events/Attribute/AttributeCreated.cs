using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Attribute
{
    public class AttributeCreated : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.AttributeCreated;
        public bool ConsumeSynchronously => true;
        public AttributeModel Attribute { get; set; }
    }
}
