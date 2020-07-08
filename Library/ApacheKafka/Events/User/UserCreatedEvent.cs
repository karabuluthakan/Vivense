using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.User
{
    public class UserCreatedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.UserCreated;

        public bool ConsumeSynchronously => false;
        public UserEventModel User { get; set; }
    }
}
