using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Library.Exceptions;
using Library.Models.Helpers;
using Microsoft.AspNetCore.Http;

namespace Library.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            } catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var exceptionType = exception.GetType();
            var message = exception.Message;
            var statusCode = exceptionType switch
            {
                var type when type == typeof(NotFoundException) => HttpStatusCode.NotFound,
                var type when type == typeof(BadRequestException) => HttpStatusCode.BadRequest,
                var type when type == typeof(BsonIdNotConvertException) => HttpStatusCode.BadRequest, 
                var type when type == typeof(UnauthorizedAccessException) => HttpStatusCode.Unauthorized, 
                _ => HttpStatusCode.BadRequest
            };

            httpContext.Response.ContentType = MediaTypeNames.Application.Json;
            httpContext.Response.StatusCode = (int) statusCode;
            return httpContext.Response.WriteAsync(new ErrorDetails(statusCode, message).ToString());
        }
    }
}