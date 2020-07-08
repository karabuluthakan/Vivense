using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events
{
    public class EmailCreatedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get;  } = PubSubEventType.EmailCreated;

        public bool ConsumeSynchronously { get;  } = true;
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; } = false;

    }

}
