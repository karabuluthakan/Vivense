using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Persistence;
using Library.Models.Dto;

namespace Library.ApacheKafka.Events
{
    public class TemplatePublishedEvent : IPubSubEvent
    {
        public PubSubEventType Topic { get; } = PubSubEventType.TemplatePublished;
        public bool ConsumeSynchronously { get; } = true;
        public PublishedTemplateDto PublishTemplate { get; set; }
    }
}
