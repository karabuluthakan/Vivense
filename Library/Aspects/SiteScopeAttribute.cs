using System.Linq;
using System.Net;
using System.Net.Mime;
using Library.CrossCuttingConcerns.Authorization.Abstract;
using Library.IoC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Aspects
{
    public class SiteScopeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var entity = ServiceTool.ServiceProvider.GetService<IEntityHierarchyProvider>();
            var entityId = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "EntityId").Value;
            if (string.IsNullOrEmpty(entityId))
            {
                context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                context.Result = new UnauthorizedObjectResult(Policy);
            }

            var hierarchy = entity.GetEntityHierarchy().Result;
            var result = hierarchy.ContainsKey(entityId);
            if (result)
            {
                context.Result = new ObjectResult(hierarchy[entityId]);
                context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.OK; 
            } else
            {
                context.Result = new UnauthorizedObjectResult("yetkiniz bulunmamaktadır");
                context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized; 
            }
        }
    }
}