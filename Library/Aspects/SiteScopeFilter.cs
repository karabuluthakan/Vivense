using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Library.CrossCuttingConcerns.Authorization.Abstract;
using Library.IoC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Aspects
{
    public class SiteScopeFilter : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var entity = ServiceTool.ServiceProvider.GetService<IEntityHierarchyProvider>();
            var entityId = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "EntityId").Value;
            var items = context.HttpContext.Request.Headers.FirstOrDefault(x=>x.Key =="opCode").Value;
            if (string.IsNullOrEmpty(entityId))
            {
                context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                context.Result = new UnauthorizedObjectResult($"yetkin yok hacı {items}");
                return;
            }

            var hierarchy = entity.GetEntityHierarchy().Result;
            var result = hierarchy.ContainsKey(entityId);
            if (!result)
            {
                context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                context.Result = new UnauthorizedObjectResult($"yetkiniz bulunmamaktadır yazdım burayada {items}");
                return;
            }

            await next();
        }
    }
}