using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Abstract
{
    public interface IPubSubEvent
    {
        PubSubEventType Topic { get; }
        bool ConsumeSynchronously { get; }
    }
}