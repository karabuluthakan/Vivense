using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Models.Abstract;

namespace Library.Utilities.QueryParameters
{
    public interface IQueryParameterApplier
    {
        Task<T> Apply<T>(IEnumerable<T> source, QueryParameter queryParameter) where T : class, IEntity, new();
    }
}