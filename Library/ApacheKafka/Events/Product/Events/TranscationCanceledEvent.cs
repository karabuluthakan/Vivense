using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Product.Events
{
    public class TranscationCanceledEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.TransactionCanceled;
        public bool ConsumeSynchronously { get; } = true;

        public string TransactionId { get; set; }
    }
}