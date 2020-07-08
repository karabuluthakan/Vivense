using System;
using System.Text.Json.Serialization;
using Library.RabbitMq.Events;
using Microsoft.Extensions.Logging;

namespace Library.CrossCuttingConcerns.Logging
{
    public class GelfLogMessageIntegrationEvent : IntegrationEvent
    {
        [JsonPropertyName("version")]
        public string Version { get; private set; } = "1.1";

        [JsonPropertyName("host")]
        public string Host { get; set; }

        [JsonPropertyName("_ip_addresses")]
        public string IpAddresses { get; set; }

        [JsonPropertyName("_x_forwarded")]
        public string XForwarded { get; set; }

        [JsonPropertyName("_environment")]
        public string Environment { get; set; }

        [JsonPropertyName("_application_name")]
        public string ApplicationName { get; set; }

        [JsonPropertyName("short_message")]
        public string ShortMessage { get; set; }

        //[JsonPropertyName("full_message")]
        //public string FullMessage { get; set; }
        [JsonPropertyName("timestamp")]
        public int Timestamp { get; private set; }

        [JsonPropertyName("totalticks")]
        public long TotalTicks { get; private set; }

        [JsonPropertyName("level")]
        public LogLevel Level { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GelfLogMessageIntegrationEvent()
        {
            var t = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            Timestamp = Convert.ToInt32(t.TotalSeconds);
            TotalTicks = t.Ticks;
        }
    }
}