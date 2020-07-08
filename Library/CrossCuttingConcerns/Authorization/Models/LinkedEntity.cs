using System.Collections.Generic;
using Library.Models.DbHelpers;
using Library.Models.Enums;
using Library.Models.Lookups;

namespace Library.CrossCuttingConcerns.Authorization.Models
{
    public class LinkedEntity
    {
        public EntityType EntityType { get; set; }
        public string ParentEntityId { get; set; }
        public string EntityId { get; set; }
        public OwnerDetail Owner { get; set; }
        public List<SharedWithDetail> SharedWith { get; set; } = new List<SharedWithDetail>();
        public List<LookupIdName> Sites { get; set; } = new List<LookupIdName>();
    }
}