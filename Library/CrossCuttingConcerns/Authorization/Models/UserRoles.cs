using System;

namespace Library.CrossCuttingConcerns.Authorization.Models
{
    public class UserRoles
    {
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public Guid RoleId { get; set; }
    }
}