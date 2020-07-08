using System.Collections.Generic;
using System.Threading;
using Confluent.Kafka;

namespace Library.ApacheKafka.Persistence
{
    public class KafkaExtendedConsumer
    {
        public IConsumer<Ignore, string> Consumer { get; set; }
        public ConsumerConfig ConsumerConfig { get; set; }
        public List<KafkaSubscription> Subscriptions { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
    }
}