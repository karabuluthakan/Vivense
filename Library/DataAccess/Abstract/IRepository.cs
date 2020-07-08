using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.CrossCuttingConcerns.HealthChecks;
using Library.CrossCuttingConcerns.HealthChecks.Abstract;
using Library.Models.Abstract;
using Library.Utilities.QueryParameters; 

namespace Library.DataAccess.Abstract
{
    public interface IRepository<T, in TKey> : IDisposable,IHealthCheckService where T : class, IEntity, new() where TKey : IEquatable<TKey>
    {
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = null);
        IQueryable<T> Get(QueryParameter parameter);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(TKey id);
        Task AddAsync(T entity);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(TKey id, T entity);
        Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate);
        Task<T> DeleteAsync(T entity);
        Task<T> DeleteAsync(TKey id);
        Task<T> DeleteAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
    }
}