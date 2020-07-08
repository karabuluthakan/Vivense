using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Library.ApacheKafka.Abstract;
using Library.CrossCuttingConcerns.Logging;

namespace Library.ApacheKafka.Persistence
{
    public class PubSubEventHandlerGenerator
    {
        public static Func<string, Task> GetEventHandlerFromDelegate<TEvent>(Func<TEvent, Task> del,
            ILoggerService logger)
        {
            return async eventData =>
            {
                var evtData = default(TEvent);

                try
                {
                    if (!string.IsNullOrWhiteSpace(eventData))
                    {
                        evtData = JsonSerializer.Deserialize<TEvent>(eventData);
                    }
                } catch (Exception ex)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Kafka event consume error.");
                    sb.AppendLine("Event data could not be cast to type " + typeof(TEvent) + ".");
                    sb.AppendLine("Event Data:");
                    sb.AppendLine(eventData);
                    sb.AppendLine("Exception Details:");
                    sb.AppendLine(ex.ToString());

                    logger.LogError(sb.ToString());
                    return;
                }


                try
                {
                    await del(evtData);
                } catch (Exception ex)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Kafka event consume error.");
                    sb.AppendLine("Event Data:");
                    sb.AppendLine(eventData);
                    sb.AppendLine("Exception Details:");
                    sb.AppendLine(ex.ToString());

                    logger.LogError(sb.ToString());
                }
            };
        }

        public static Func<TParameter, Task> GetEventHandlerRemoveSubscribeAfterDone<TParameter>(IPublisherSubscriber
                publisher,
            string subscribeId,
            Func<TParameter, Task> func)
        {
            return async result =>
            {
                try
                {
                    await func(result);
                } catch (Exception e)
                {
                    try
                    {
                        publisher.Unsubscribe(subscribeId);
                    } catch (Exception)
                    {
                        //ignored
                    }

                    throw;
                }

                publisher.Unsubscribe(subscribeId);
            };
        }
    }
}