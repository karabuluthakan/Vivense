using System;
using Newtonsoft.Json;

namespace Library.RabbitMq.Events
{
    public class IntegrationEvent
    {
        [JsonProperty("_message_id")]
        public Guid Id { get; private set; }

        [JsonProperty("_creation_date")]
        public DateTime CreationDate { get; private set; }

        /// <summary>
        ///     Constructor
        /// </summary>
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }
    }
}