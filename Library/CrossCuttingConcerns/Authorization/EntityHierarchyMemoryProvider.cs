using System.Collections.Generic;
using System.Threading.Tasks;
using Library.CrossCuttingConcerns.Authorization.Abstract;
using Library.CrossCuttingConcerns.Authorization.Models;
using Library.Models.DbHelpers;
using Library.Models.Enums;
using Library.Models.Lookups;
#pragma warning disable 1998

namespace Library.CrossCuttingConcerns.Authorization
{
    public class EntityHierarchyMemoryProvider : IEntityHierarchyProvider
    {
        public async Task<IDictionary<string, List<LinkedEntity>>> GetEntityHierarchy()
        {
            var cache = new Dictionary<string, List<LinkedEntity>>();

            for (var i = 0; i < 100; i++)
            {
                cache.Add(i.ToString(),new List<LinkedEntity>
                {
                    new LinkedEntity
                    {
                        EntityId = $"EntityId {i}", EntityType = EntityType.Carrier, ParentEntityId = $"ParentEntityId {i}",
                        Owner = new OwnerDetail
                        {
                            EntityId = $"Owner EntityId {i}", UserId = $"Owner UserId {i}",
                            SiteInfo = new LookupIdName {Id = $"SiteInfo Id {i}", Name = $"SiteInfo Name {i}"}
                        },
                        SharedWith = new List<SharedWithDetail>
                        {
                            new SharedWithDetail
                            {
                                UserId = $"SharedWithDetail UserId {i}",
                                SiteInfo = new LookupIdName {Id = $"SharedWithDetail Id {i}", Name = $"SharedWithDetail Name {i}"},
                                EntityId = $"SharedWithDetail EntityId {i}"
                            }
                        },
                        Sites = new List<LookupIdName> {new LookupIdName {Id = $"Sites Id {i}", Name = $"Sites Name {i}"}}
                    }
                });
            }

            return cache;
        }
    }
}