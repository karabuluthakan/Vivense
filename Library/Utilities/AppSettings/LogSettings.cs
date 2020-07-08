using Microsoft.Extensions.Logging;

namespace Library.Utilities.AppSettings
{
    public class LogSettings
    {
        public const string LogThresholdKey = nameof(LogThreshold);
        public LogLevel LogThreshold { get; set; }
    }
}