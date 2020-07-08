using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Library.Middlewares
{
    public class ResponseRewindMiddleware
    {
        private readonly RequestDelegate next;

        public ResponseRewindMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;

            try
            {
                await using var memoryStream = new MemoryStream();
                context.Response.Body = memoryStream;

                await next(context);

                memoryStream.Position = 0;
                var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(originalBody);
            } finally
            {
                context.Response.Body = originalBody;
            }
        }
    }
}