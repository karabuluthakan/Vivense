using System;
using System.Collections.Generic;

namespace Library.CrossCuttingConcerns.Authorization.Models
{
    public class EntityList
    {
        public Guid Id { get; set; }
        public IReadOnlyCollection<EntityWithSiteDescendants> Data { get; set; } = new EntityWithSiteDescendants[0];

        public EntityList()
        {

        }

        public EntityList(Guid id, IReadOnlyCollection<EntityWithSiteDescendants> data)
        {
            this.Data = data;
            this.Id = id;
        }

        public EntityList(IReadOnlyCollection<EntityWithSiteDescendants> data)
        {
            Id = Guid.NewGuid();
            this.Data = data;
        }
    }
}