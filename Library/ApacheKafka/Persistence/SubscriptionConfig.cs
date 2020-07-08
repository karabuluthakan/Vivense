using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Confluent.Kafka;
using Library.ApacheKafka.Abstract;

namespace Library.ApacheKafka.Persistence
{
    public class SubscriptionConfig : ISubscriptionConfig
    {
        public string Id { get; }
        public string Topic { get; private set; }
        public string GroupId { get; }
        public AutoOffsetReset StartOfProcessing { get; }

        [JsonIgnore]
        public Func<string, Task> OnEventReceived { get; }

        public bool ConsumeEventsSynchronously { get; }
        private bool suffixAdded = false;

        public void AddTopicSuffix(string suffix)
        {
            if (!suffixAdded)
            {
                Topic += suffix;
                suffixAdded = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleEventInstance">Sample event instance to subscribe. Simply create and pass in a new instance of the event type.</param>
        /// <param name="startOfProcessing">Start from beginning or end if partition has no committed offset.</param>
        /// <param name="onEventReceived">Message handler</param>
        /// <param name="groupId">Consumer group id. Pass null to use the service instance specific group id.</param>
        public SubscriptionConfig(IPubSubEvent sampleEventInstance, string groupId, AutoOffsetReset startOfProcessing,
            Func<string, Task> onEventReceived)
        {
            Id = Guid.NewGuid().ToString();
            Topic = sampleEventInstance.Topic.ToString();
            GroupId = groupId;
            StartOfProcessing = startOfProcessing;
            OnEventReceived = onEventReceived;
            ConsumeEventsSynchronously = sampleEventInstance.ConsumeSynchronously;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">subscription config id</param>
        /// <param name="sampleEventInstance">Sample event instance to subscribe. Simply create and pass in a new instance of the event type.</param>
        /// <param name="startOfProcessing">Start from beginning or end if partition has no committed offset.</param>
        /// <param name="onEventReceived">Message handler</param>
        /// <param name="groupId">Consumer group id. Pass null to use the service instance specific group id.</param>
        public SubscriptionConfig(string id, IPubSubEvent sampleEventInstance, string groupId, AutoOffsetReset startOfProcessing,
            Func<string, Task> onEventReceived)
        {
            Id = id;
            Topic = sampleEventInstance.Topic.ToString();
            GroupId = groupId;
            StartOfProcessing = startOfProcessing;
            OnEventReceived = onEventReceived;
            ConsumeEventsSynchronously = sampleEventInstance.ConsumeSynchronously;
        }
    }
}