using System;
using System.Collections.Generic;

namespace Library.ApacheKafka.Events.Scheduler
{
    public class BaseSchedulerData
    {
        public string GroupName { get; set; }
    }

    public class SchedulerJob : BaseSchedulerData
    {
        public string JobName { get; set; }
        public List<ScheduleTrigger> Triggers { get; set; }


        public dynamic InnerEventToBePublished { get; set; }
    }

    public class ScheduleTrigger : BaseSchedulerData
    {
        public string TriggerName { get; set; }
        public DateTime? StartTimeUtc { get; set; }
        public DateTime? EndTimeUtc { get; set; }
        public int IntervalInMillis { get; set; }
        public ScheduleStrategy ScheduleStrategy { get; set; }
        public DayOfWeek? DayOfWeek { get; set; }
        public TimeSpan? TimeOfDay { get; set; }
    }
}