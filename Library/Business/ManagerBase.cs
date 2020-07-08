using System;
using System.Threading.Tasks;
using Library.Models.Dto;
using Library.Utilities.QueryParameters;
using Library.Utilities.Results.Abstract;

namespace Library.Business
{
    public abstract class ManagerBase<TCreate, TUpdate, TKey> : IServiceBase<TCreate, TUpdate, TKey>
        where TCreate : class, IDto, new() 
        where TUpdate : class, IDto, new() 
        where TKey : IEquatable<TKey>
    {
        public IResult List(QueryParameter parameter)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> ShowItem(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Create(TCreate entity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Update(TKey id,TUpdate entity)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Delete(TKey id)
        {
            throw new NotImplementedException();
        }
    }
}