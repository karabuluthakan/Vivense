using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Library.ApacheKafka.Abstract
{
    public interface ISubscriptionConfig
    {
        string Id { get; }
        string Topic { get; }
        string GroupId { get; }
        AutoOffsetReset StartOfProcessing { get;}
        Func<string, Task> OnEventReceived { get; }
        bool ConsumeEventsSynchronously { get; }
        void AddTopicSuffix(string suffix);
    }
}