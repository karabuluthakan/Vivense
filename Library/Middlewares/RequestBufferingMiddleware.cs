using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Library.Middlewares
{
    public class RequestBufferingMiddleware
    {
        private readonly RequestDelegate next;

        public RequestBufferingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context?.Request?.EnableBuffering();

            var syncIOFeature = context?.Features?.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }

            await next(context);
        }
    }
}