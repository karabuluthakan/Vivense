using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events
{
   public class KafkaHealthCheckEvent :  IPubSubEvent
   {
        public PubSubEventType Topic { get; } = PubSubEventType.KafkaHealthCheck;
        public bool ConsumeSynchronously { get; } = false;
    }
}
