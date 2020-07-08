using System.Collections.Generic;
using System.Threading.Tasks;
using Library.CrossCuttingConcerns.Authorization.Models;

namespace Library.CrossCuttingConcerns.Authorization.Abstract
{
    public interface IEntityHierarchyProvider
    {
        Task<IDictionary<string, List<LinkedEntity>>> GetEntityHierarchy();
    }
}