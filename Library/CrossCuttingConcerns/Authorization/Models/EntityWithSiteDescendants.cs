using System.Collections.Generic;
using Library.CrossCuttingConcerns.Validation;

namespace Library.CrossCuttingConcerns.Authorization.Models
{
    public class EntityWithSiteDescendants 
    {
        [BsonIdValidate]
        public string EntityId { get; set; }

        public List<LinkedEntity> LinkedEntities { get; set; } = new List<LinkedEntity>();
    }
}