using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Scheduler
{

   
    public class SchedulerJobDeletionRequestedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.SchedulerJobDeletionRequested;
        public bool ConsumeSynchronously { get; } = true;
        public string Id { get; set; }
        public string Group { get; set; }
    }
}
