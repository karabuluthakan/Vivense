using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Library.Middlewares
{
    public class AuthorizeHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public AuthorizeHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            
        }
    }
}