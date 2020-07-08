using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.CrossCuttingConcerns.Authorization;
using Library.CrossCuttingConcerns.Authorization.Abstract;
using Library.CrossCuttingConcerns.Authorization.Enums;
using Library.CrossCuttingConcerns.Authorization.Models;
using Library.Extensions;
using Library.IoC;
using Library.Models.Lookups;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Aspects
{
    public class ScopeAuthorizeAttribute : IAsyncActionFilter
    {
        private readonly ScopeType scopeType;
        private readonly string operationCode;
        private readonly IEntityHierarchyProvider hierarchyProvider; 

        public ScopeAuthorizeAttribute(ScopeType scopeType, string operationCode)
        {
            this.scopeType = scopeType;
            this.operationCode = operationCode;
            this.hierarchyProvider = ServiceTool.ServiceProvider.GetService<IEntityHierarchyProvider>();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var permissionScope = context.HttpContext.GetPermissionScope(operationCode);
            var hierarchy = await HierarchyInScope(context);
            switch (permissionScope)
            {
                case PermissionScope.None:
                    var d = context?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == operationCode)?.Value;
                    break;
                case PermissionScope.User:
                    var result = PermissionScopeUser(context, hierarchy);
                    if (!result)
                    {
                        throw new UnauthorizedAccessException();
                    }

                    await next();
                    break;

                case PermissionScope.BusinessUnit:
                    await PermissionScopeBusinessUnit(context, hierarchy, next);
                    break;
                case PermissionScope.BusinessUnitAndChildren:
                    await PermissionScopeBusinessUnitAndChildren(context, hierarchy, next);
                    break;
                case PermissionScope.Organization:
                    await PermissionScopeOrganization(context, hierarchy, next);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool PermissionScopeUser(ActionExecutingContext context, List<LinkedEntity> hierarchy)
        {
            switch (scopeType)
            {
                case ScopeType.SiteSpecific:
                {
                    var querySites = context.HttpContext.GetRequestSites();
                    return hierarchy.Any(x => x.Sites.Any(q => querySites.Contains(q.Id)));
                }
                case ScopeType.OwnerSpecific:
                {
                    var userId = context.HttpContext.GetUserId();
                    return hierarchy.Any(x => x.Owner.UserId.Equals(userId) || x.SharedWith.Any(sw => sw.UserId.Equals(userId)));
                }
                case ScopeType.BothSiteAndOwnerSpecific:
                {
                    var userId = context.HttpContext.GetUserId();
                    var querySites = context.HttpContext.GetRequestSites();
                    return hierarchy.Any(x =>
                        (x.Owner.UserId.Equals(userId)
                         || x.SharedWith.Any(sw => sw.UserId.Equals(userId))
                         || querySites.Contains(x.Owner.SiteInfo.Id)
                         || x.SharedWith.Any(sw => querySites.Contains(sw.SiteInfo.Id)))
                        && x.Sites.Any(q => querySites.Contains(q.Id)));
                }
                case ScopeType.SiteAndOwnerNonSpecific:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }

        private async Task PermissionScopeBusinessUnit(ActionExecutingContext context, List<LinkedEntity> hierarchy, ActionExecutionDelegate next)
        {
            throw new NotImplementedException();
        }

        private async Task PermissionScopeBusinessUnitAndChildren(ActionExecutingContext context, List<LinkedEntity> hierarchy,
            ActionExecutionDelegate next)
        {
            throw new NotImplementedException();
        }

        private async Task PermissionScopeOrganization(ActionExecutingContext context, List<LinkedEntity> hierarchy, ActionExecutionDelegate next)
        {
            throw new NotImplementedException();
        }

        private async Task<List<LinkedEntity>> HierarchyInScope(ActionExecutingContext context)
        {
            var entities = context.HttpContext.GetUserEntityIds();
            var linkedEntities = new List<LinkedEntity>();
            var hierarchy = await hierarchyProvider.GetEntityHierarchy();
            foreach (var linked in entities.Select(entity => hierarchy[entity]))
            {
                linkedEntities.AddRange(linked);
            }

            return linkedEntities;
        }

        private async Task<List<string>> GetUserSites(ActionExecutingContext context)
        {
            var linkedEntities = await HierarchyInScope(context);
            var userSites = new List<LookupIdName>(linkedEntities.Count);
            foreach (var linked in linkedEntities)
            {
                userSites.AddRange(linked.Sites);
            }

            var querySites = context.HttpContext.GetRequestSites();
            return querySites.Length == 0
                ? userSites.Select(x => x.Id).Distinct().ToList()
                : userSites.Where(x => querySites.Contains(x.Id)).Select(s => s.Id).Distinct().ToList();
        }
    }
}