using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace Library.Utilities.AuditServices
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditManager : IAuditService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IHostEnvironment hostEnvironment;
        private const int bufferSize = 1024;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="hostEnvironment"></param>
        public AuditManager(IHttpContextAccessor httpContextAccessor, IHostEnvironment hostEnvironment)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetStatusCode()
        {
            return httpContextAccessor.HttpContext.Response.StatusCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetUserId()
        {
            return httpContextAccessor?.HttpContext?.User?.Claims?.SingleOrDefault(x => x.Type == "userId")?.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetUserName()
        {
            return httpContextAccessor?.HttpContext?.User?.Claims?.SingleOrDefault(x => x.Type == "username")?.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetMethodName()
        {
            return httpContextAccessor?.HttpContext?.Request?.Method;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetPathName()
        {
            return httpContextAccessor?.HttpContext?.Request?.Path;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetPathBase()
        {
            return httpContextAccessor?.HttpContext?.Request?.PathBase;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetProtocolName()
        {
            return httpContextAccessor?.HttpContext?.Request?.Protocol;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSchemeName()
        {
            return httpContextAccessor?.HttpContext?.Request?.Scheme;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetHostName()
        {
            return $"{httpContextAccessor?.HttpContext?.Request?.Scheme}://{httpContextAccessor?.HttpContext?.Request?.Host}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetXForwarded()
        {
            httpContextAccessor?.HttpContext?.Request?.Headers?.TryGetValue("X-Forwarded-For", out var xForwarded);
            return xForwarded.LastOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetIpAddresses()
        {
            var ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            return JsonSerializer.Serialize(ipHostEntry?.AddressList.Select(a => a.ToString()).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetEnvironmentName()
        {
            return hostEnvironment?.EnvironmentName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetApplicationName()
        {
            return hostEnvironment?.ApplicationName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HttpRequest GetHttpRequest()
        {
            return httpContextAccessor?.HttpContext?.Request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HttpResponse GetHttpResponse()
        {
            return httpContextAccessor?.HttpContext?.Response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IFormCollection TryGetFormData()
        {
            IFormCollection form = null;
            var request = httpContextAccessor?.HttpContext?.Request;
            try
            {
                if (!request.HasFormContentType)
                {
                    return null;
                }

                form = request.Form;
            } catch
            {
                // ignored
            }

            return form;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetResponseBodyString()
        {
            var responseBody = httpContextAccessor?.HttpContext?.Response?.Body;
            if (responseBody == null)
            {
                return null;
            }

            string returnBody;

            try
            {
                using var reader = new StreamReader(responseBody, Encoding.UTF8, true, bufferSize, true);
                responseBody.Seek(0, SeekOrigin.Begin);
                returnBody = await reader.ReadToEndAsync();
            } catch (Exception e)
            {
                returnBody = $"Exception occured while trying to retrieve the response body. => {e}";
            }

            return returnBody;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetRequestBodyString()
        {
            var request = httpContextAccessor?.HttpContext?.Request;
            var requestBody = request.Body;
            if (requestBody == null)
            {
                return null;
            }

            string responseBody = null;
            try
            {
                using var reader = new StreamReader(requestBody, Encoding.UTF8, true, bufferSize, true);
                requestBody.Seek(0, SeekOrigin.Begin);
                responseBody = await reader.ReadToEndAsync();
            } catch (Exception e)
            {
                responseBody = $"Exception occured while trying to retrieve the request body. => {e}";
            }

            return responseBody;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public QueryString GetQueryString()
        {
            return httpContextAccessor.HttpContext.Request.QueryString;
        }
    }
}