using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.User
{
    public class UserUpdatedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.UserUpdated;
        public bool ConsumeSynchronously => false;
        public UserEventModel User { get; set; }
    }
}
