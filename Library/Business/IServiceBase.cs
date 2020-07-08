using System;
using System.Threading.Tasks;
using Library.Models.Dto;
using Library.Utilities.QueryParameters;
using Library.Utilities.Results.Abstract;

namespace Library.Business
{
    public interface IServiceBase<in TCreate, in TUpdate, in TKey>
        where TCreate : class, IDto, new()
        where TUpdate : class, IDto, new()
        where TKey : IEquatable<TKey>
    {
        IResult List(QueryParameter parameter);
        Task<IResult> ShowItem(TKey key);
        Task<IResult> Create(TCreate entity);
        Task<IResult> Update(TKey id,TUpdate entity);
        Task<IResult> Delete(TKey id);
    }
}