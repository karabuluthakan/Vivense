﻿using Library.ApacheKafka.Abstract;
 using Library.ApacheKafka.Persistence;
 using Microsoft.Extensions.Logging;

 namespace Library.ApacheKafka.Events
{
    public class LogTresholdChangedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.LogThresholdChanged;
        public bool ConsumeSynchronously { get; } = false;
        public LogLevel NewLogTreshold { get; set; }
    }
}
