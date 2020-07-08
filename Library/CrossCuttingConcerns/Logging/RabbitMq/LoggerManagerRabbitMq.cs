using System.Threading.Tasks;
using Library.RabbitMq.Abstract;
using Library.Utilities.AppSettings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Library.CrossCuttingConcerns.Logging.RabbitMq
{
    public class LoggerManagerRabbitMq
    {
        private readonly IIntegrationEventService eventBus;
        private LogLevel logThreshold;

        public LoggerManagerRabbitMq(IIntegrationEventService eventBus, IOptions<LogSettings> options)
        {
            this.eventBus = eventBus;
            logThreshold = options.Value.LogThreshold;
        }

        private async Task LogGelf(GelfLogMessageIntegrationEvent message, bool isAudit = false)
        {
            if (((int) message.Level < (int) logThreshold || message.Level == LogLevel.None) && !isAudit)
            {
                return;
            }

            await eventBus.PublishThroughEventBusAsync(message);
        }

        public virtual async Task Log(LogLevel level, string message, string host, string ipAddress, string hostingEnvironmentStr,
            string applicationName, string xForwarded, bool isAudit = false)
        {
            await LogGelf(new GelfLogMessageIntegrationEvent()
            {
                Level = level,
                ShortMessage = message,
                Host = host,
                IpAddresses = ipAddress,
                Environment = hostingEnvironmentStr,
                ApplicationName = applicationName,
                XForwarded = xForwarded
            }, isAudit);
        }
    }
}