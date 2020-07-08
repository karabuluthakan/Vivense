using System;
using System.Threading.Tasks;

namespace Library.ApacheKafka.Persistence
{
    public class KafkaSubscription
    {
        public string SubscriptionId { get; set; }
        public string Topic { get; set; }
        public Func<string, Task> Handler { get; set; }
    }
}