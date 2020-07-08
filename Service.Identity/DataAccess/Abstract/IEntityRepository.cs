using System.Collections.Generic;
using System.Threading.Tasks;
using Library.CrossCuttingConcerns.Authorization.Models;
using Library.DataAccess.Abstract;
using Service.Identity.Models;

namespace Service.Identity.DataAccess.Abstract
{
    public interface IEntityRepository : IRepository<Entity, string>
    {
        Task<IReadOnlyCollection<EntityWithSiteDescendants>> GetEntitiesHierarchy();
    }
}