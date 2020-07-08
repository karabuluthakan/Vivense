﻿
using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;

namespace Library.ApacheKafka.Events.Entity
{
    public class EntityListCrudChangedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.EntityListCrudChanged;
        public bool ConsumeSynchronously { get; } = false;
    }
}