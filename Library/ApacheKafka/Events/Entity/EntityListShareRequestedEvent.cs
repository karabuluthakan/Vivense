﻿
using System;
using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Entity
{
    public class EntityListShareRequestedEvent : IPubSubEvent
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public PubSubEventType Topic => PubSubEventType.EntityListShareRequested;
        public bool ConsumeSynchronously => true;
    
    }
}
