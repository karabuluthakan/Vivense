using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Library.CrossCuttingConcerns.Logging
{
    public interface ILoggerService
    {
        Task Log(LogLevel level, string message, bool isAudit = false);
        Task Log(string message);
        Task LogTrace(string message);
        Task LogDebug(string message);
        Task LogInformation(string message);
        Task LogWarning(string message);
        Task LogError(string message);
        Task LogCritical(string message);
        Task LogRequestForAudit(bool logGets = false);
    }
}