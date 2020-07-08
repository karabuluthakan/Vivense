using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Scheduler
{
    public class SchedulerJobCreationRequestedEvent : SchedulerJobEventBase, IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.SchedulerJobCreationRequested;
        public bool ConsumeSynchronously { get; } = true;
    }
}
