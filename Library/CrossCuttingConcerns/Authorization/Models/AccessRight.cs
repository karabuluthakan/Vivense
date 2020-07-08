using System;
using Library.CrossCuttingConcerns.Authorization.Enums;

namespace Library.CrossCuttingConcerns.Authorization.Models
{
    public class AccessRight
    {
        public string Code { get; set; }

        public int Scope { get; set; } = 2;

        public PermissionScope ScopeAsEnum()
        {
            if (Enum.IsDefined(typeof(PermissionScope), Scope))
            {
                return (PermissionScope) (Scope);
            }

            return PermissionScope.None;
        }

        public static int OrganizationWideScope()
        {
            return (int) PermissionScope.Organization;
        }
    }
}