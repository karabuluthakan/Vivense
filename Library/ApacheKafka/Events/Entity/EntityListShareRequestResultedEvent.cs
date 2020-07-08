﻿ using Library.ApacheKafka.Abstract;
  using Library.ApacheKafka.Persistence;
  using Library.CrossCuttingConcerns.Authorization.Models;

  namespace Library.ApacheKafka.Events.Entity
{
    public class EntityListShareRequestResultedEvent : IPubSubEvent
    {
        public PubSubEventType Topic => PubSubEventType.EntityListShareRequestResulted;

        public bool ConsumeSynchronously => false;

        public EntityList List { get; set; }

        public string RequestId { get; set; } = "";
        public EntityListShareRequestResultedEvent(EntityList entityList,string requestId)
        {
             List = entityList;
            RequestId = requestId;
        }
        
        public EntityListShareRequestResultedEvent()
        {
            List = new EntityList();
        }
    }
}
