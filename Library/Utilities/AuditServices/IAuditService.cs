using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Library.Utilities.AuditServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuditService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int GetStatusCode();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetUserId();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetUserName();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetMethodName();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetPathName();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetPathBase();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetProtocolName();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetSchemeName();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetHostName();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetXForwarded();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetIpAddresses();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetEnvironmentName();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetApplicationName();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        HttpRequest GetHttpRequest();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        HttpResponse GetHttpResponse();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IFormCollection TryGetFormData();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<string> GetResponseBodyString();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<string> GetRequestBodyString();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        QueryString GetQueryString();
    }
}