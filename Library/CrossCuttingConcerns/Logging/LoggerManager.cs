using System;
using System.Text.Json;
using System.Threading.Tasks;
using Library.CrossCuttingConcerns.Logging.RabbitMq;
using Library.RabbitMq.Abstract;
using Library.Utilities.AppSettings;
using Library.Utilities.AuditServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Library.CrossCuttingConcerns.Logging
{
    public class LoggerManager : LoggerManagerRabbitMq, ILoggerService
    {
        private readonly IAuditService auditService;

        public LoggerManager(IIntegrationEventService eventBus, IOptions<LogSettings> options, IAuditService auditService) : base(eventBus, options)
        {
            this.auditService = auditService;
        }

        public async Task Log(LogLevel level, string message, bool isAudit = false)
        {
            try
            {
                var host = auditService.GetHostName();
                var ipAddresses = auditService.GetIpAddresses();
                var xForwarded = auditService.GetXForwarded();
                var environmentName = auditService.GetEnvironmentName();
                var applicationName = auditService.GetApplicationName();
                await base.Log(level, message, host, ipAddresses, environmentName, applicationName, xForwarded, isAudit);
            } catch (Exception e)
            {
                try
                {
                    _ = base.Log(level, e.ToString(), "unknown host", "0.0.0.0", "", "", "", isAudit);
                } catch (Exception exception)
                {
                    Console.WriteLine($"{exception}");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task Log(string message)
        {
            await Log(LogLevel.Information, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task LogTrace(string message)
        {
            await Log(LogLevel.Trace, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task LogDebug(string message)
        {
            await Log(LogLevel.Debug, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task LogInformation(string message)
        {
            await Log(LogLevel.Information, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task LogWarning(string message)
        {
            await Log(LogLevel.Warning, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task LogError(string message)
        {
            await Log(LogLevel.Error, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task LogCritical(string message)
        {
            await Log(LogLevel.Critical, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logGets"></param>
        /// <returns></returns>
        public async Task LogRequestForAudit(bool logGets = false)
        {
            try
            {
                var method = auditService.GetMethodName();
                if (method == HttpMethods.Get && !logGets || method == HttpMethods.Options)
                {
                    return;
                }

                var logMessage = new
                {
                    userId = auditService.GetUserId(),
                    userName = auditService.GetUserName(),
                    requestBody = await auditService.GetRequestBodyString(),
                    requestForm = auditService.TryGetFormData(),
                    requestHost = auditService.GetHostName(),
                    requestMethod = auditService.GetMethodName(),
                    requestPath = auditService.GetPathName(),
                    requestPathBase = auditService.GetPathBase(),
                    requestProtocol = auditService.GetProtocolName(),
                    requestQueryString = auditService.GetQueryString(),
                    requestScheme = auditService.GetSchemeName(),
                    responseStatusCode = auditService.GetStatusCode(),
                    responseBody = await auditService.GetResponseBodyString()
                };

                var message = JsonSerializer.Serialize(logMessage);
                await Log(LogLevel.Trace, message, true);
            } catch (Exception e)
            {
                await LogError($"{e}");
            }
        }
    }
}