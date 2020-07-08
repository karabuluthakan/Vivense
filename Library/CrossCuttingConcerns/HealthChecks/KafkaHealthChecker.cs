using System.Net;
using System.Threading.Tasks;
using Confluent.Kafka;
using Library.ApacheKafka.Abstract;
using Library.ApacheKafka.Events;
using Library.CrossCuttingConcerns.HealthChecks.Abstract;
using Library.Utilities.Results;
using Library.Utilities.Results.Abstract;

namespace Library.CrossCuttingConcerns.HealthChecks
{
    public class KafkaHealthChecker : IKafkaHealthChecker
    {
        private readonly IPublisherSubscriber publisherSubscriber;

        public KafkaHealthChecker(IPublisherSubscriber publisherSubscriber)
        {
            this.publisherSubscriber = publisherSubscriber;
        }

        public async Task<IResult> GetKafkaStatus()
        {
            if (publisherSubscriber == null)
            {
                return new Result(HttpStatusCode.BadRequest, $"{nameof(publisherSubscriber)} is null");
            }

            var kafkaHealthCheckEvent = new KafkaHealthCheckEvent();
            var isPublish = await publisherSubscriber.PublishAsync(kafkaHealthCheckEvent);
            if (isPublish == null)
            {
                return new Result(HttpStatusCode.BadRequest, "Not published");
            } else if (isPublish.Status == PersistenceStatus.Persisted)
            {
                return new Result(HttpStatusCode.OK, "Successfully");
            } else
            {
                return new Result(HttpStatusCode.BadRequest, "Kafka is not working!");
            }
        }
    }
}