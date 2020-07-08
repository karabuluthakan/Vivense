using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.User
{
    public class UserDeleteEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.UserDeleted;

        public bool ConsumeSynchronously => false;

        public string UserEmail { get; set; }
    }
}
