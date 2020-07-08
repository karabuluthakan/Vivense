using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Library.ApacheKafka.Abstract
{
    public interface IPublisherSubscriber
    {
        void Publish(IPubSubEvent evt);
        Task<DeliveryResult<Null, string>> PublishAsync(IPubSubEvent evt);
        void Subscribe(List<ISubscriptionConfig> subscriptionConfigs);
        void Unsubscribe(string subscriptionConfigId);
        string TopicSuffix { get; }
    }
}