using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events
{
    public class CommissionRateChangedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.CommissionRateChanged;
        public bool ConsumeSynchronously { get; } = true;
        public string ProductId { get; set; }
    }
}
