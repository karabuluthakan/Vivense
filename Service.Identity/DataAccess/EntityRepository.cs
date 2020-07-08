using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.CrossCuttingConcerns.Authorization.Models;
using Library.DataAccess.MongoDb;
using Library.Models.Enums;
using Library.Models.Lookups;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Service.Identity.DataAccess.Abstract;
using Service.Identity.DataAccess.Persistence;
using Service.Identity.Models;

namespace Service.Identity.DataAccess
{
    public class EntityRepository : MongoDbRepositoryBase<Entity, string>, IEntityRepository
    {
        public EntityRepository(IOptions<IdentityMongoDbSettings> options) : base(options)
        {
        }

        public async Task<IReadOnlyCollection<EntityWithSiteDescendants>> GetEntitiesHierarchy()
        {
            const string aliases = "childEntities";
            const string connectToField = "parentEntityId";
            const string grantedSites = "grantedSites";
            const string key = "_id";
            const string name = "name";
            const string type = "type";
            const string startWith = "$_id";
            var queries = await Collection
                .Aggregate()
                .Match(s => !s.Deleted)
                .GraphLookup(Collection, key, connectToField, startWith, aliases)
                .Project(s => new BsonDocument()
                {
                    {key, 1},
                    {name, 1},
                    {type, 1},
                    {grantedSites, 1},
                    {$"{aliases}.{key}", 1},
                    {$"{aliases}.{name}", 1},
                    {$"{aliases}.{type}", 1},
                    {$"{aliases}.{grantedSites}", 1},
                }).ToListAsync();

            var allEntities = new List<EntityWithSiteDescendants>();
            foreach (var query in queries)
            {
                var entity = new EntityWithSiteDescendants()
                {
                    EntityId = query[key].ToString()
                };

                var entitySitesArray = query[grantedSites]?.AsBsonArray;
                var entitySites = new List<LookupIdName>();
                for (var i = 0; i < entitySitesArray?.Count; i++)
                {
                    var entitySite = entitySitesArray[i].AsBsonDocument;
                    entitySites.Add(new LookupIdName
                    {
                        Id = entitySite[key].ToString(),
                        Name = entitySite[name].AsString
                    });
                }

                entity.LinkedEntities.Add(new LinkedEntity()
                {
                    EntityId = query[key].ToString(),
                    EntityType = Enum.Parse<EntityType>(query[type].ToString()),
                    Sites = entitySites
                });

                var childEntities = query[aliases]?.AsBsonArray;
                for (var i = 0; i < childEntities?.Count; i++)
                {
                    var childEntity = childEntities[i].AsBsonDocument;
                    var entityType = Enum.Parse<EntityType>(childEntity[type].ToString());
                    var childEntitySitesArray = childEntity[grantedSites]?.AsBsonArray;
                    var childEntitySites = new List<LookupIdName>();
                    for (var j = 0; j < childEntitySitesArray?.Count; j++)
                    {
                        var childEntitySite = childEntitySitesArray[j].AsBsonDocument;
                        childEntitySites.Add(new LookupIdName
                        {
                            Id = childEntitySite[key].ToString(),
                            Name = childEntitySite[name].AsString
                        });
                    }

                    entity.LinkedEntities.Add(new LinkedEntity
                    {
                        EntityId = childEntity[key].ToString(),
                        EntityType = entityType,
                        ParentEntityId = query[key].ToString(),
                        Sites = childEntitySites
                    });
                }

                allEntities.Add(entity);
            }

            return allEntities;
        }
    }
}