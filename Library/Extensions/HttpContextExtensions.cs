using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Library.CrossCuttingConcerns.Authorization.Enums;
using Library.CrossCuttingConcerns.Authorization.Models;
using Library.Utilities.Assets;
using Microsoft.AspNetCore.Http;

namespace Library.Extensions
{
    public static class HttpContextExtensions
    {
        public static List<UserEntity> GetUserEntities(this HttpContext httpContext)
        {
            var entities = httpContext?.User?.Claims?.FirstOrDefault(x => x.Type == VivenseClaimType.Entities)?.Value;
            List<UserEntity> userEntities;
            try
            {
                userEntities = JsonSerializer.Deserialize<List<UserEntity>>(entities);
            } catch
            {
                userEntities = new List<UserEntity>();
            }

            return userEntities;
        }

        public static IEnumerable<string> GetUserEntityIds(this HttpContext httpContext)
        {
            var entities = httpContext?.User?.Claims?.FirstOrDefault(x => x.Type == VivenseClaimType.Entities)?.Value;
            var userEntities = JsonSerializer.Deserialize<List<UserEntity>>(entities);
            return userEntities.Select(x => x.Id).ToList();
        }

        public static IEnumerable<AccessRight> GetOperationCodes(this HttpContext httpContext)
        {
            var zippedUserOperationCodes = httpContext?.User?.Claims?.FirstOrDefault(x => x.Type == VivenseClaimType.Opcodes)?.Value;
            if (string.IsNullOrEmpty(zippedUserOperationCodes))
            {
                httpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                httpContext.Response.ContentType = MediaTypeNames.Application.Json;
                throw new UnauthorizedAccessException();
            }

            var unzipped = GZipper.Unzip(zippedUserOperationCodes);
            var result = JsonSerializer.Deserialize<List<AccessRight>>(unzipped);
            return result;
        }

        public static string[] GetRequestSites(this HttpContext httpContext)
        {
            var sitesQuery = httpContext?.Request?.Query?.Where(s => s.Key == "_sites").FirstOrDefault();
            return sitesQuery != null ? sitesQuery?.Value.FirstOrDefault()?.Split(";") : new string[] { };
        }

        public static string GetUserId(this HttpContext httpContext)
        {
            return httpContext?.User?.Claims?.SingleOrDefault(c => c.Type == VivenseClaimType.UserId)?.Value;
        } 

        public static PermissionScope GetPermissionScope(this HttpContext httpContext,string operationCode)
        {
            var zippedUserOperationCodes = httpContext?.User?.Claims?.FirstOrDefault(x => x.Type == VivenseClaimType.Opcodes)?.Value;
            var unzipped = GZipper.Unzip(zippedUserOperationCodes);
            var accessRights = JsonSerializer.Deserialize<List<AccessRight>>(unzipped);
            return (from access in accessRights where access.Code.Equals(operationCode) select access.ScopeAsEnum()).FirstOrDefault();
        }
    }
}