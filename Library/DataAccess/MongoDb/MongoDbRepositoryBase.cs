using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Library.DataAccess.Abstract;
using Library.Extensions;
using Library.Models.Abstract;
using Library.Utilities.AppSettings;
using Library.Utilities.QueryParameters;
using Library.Utilities.Results;
using Library.Utilities.Results.Abstract;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

#pragma warning disable 1998

namespace Library.DataAccess.MongoDb
{
    public abstract class MongoDbRepositoryBase<T, TKey> : IRepository<T, TKey> where T : class, IEntity<TKey>, new() where TKey : IEquatable<TKey>
    {
        protected readonly IMongoCollection<T> Collection;
        protected readonly MongoDbSettings mongoDbSettings;
        protected readonly IMongoDatabase MongoDatabase;

        protected MongoDbRepositoryBase(IOptions<MongoDbSettings> options)
        {
            this.mongoDbSettings = options.Value;
            var client = new MongoClient(mongoDbSettings.ConnectionString);
            MongoDatabase = client.GetDatabase(mongoDbSettings.DefaultDatabase);
            Collection = MongoDatabase.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? Collection.AsQueryable() : Collection.AsQueryable().Where(predicate);
        }

        public virtual IQueryable<T> Get(QueryParameter parameter)
        {
            return Collection.AsQueryable().QueryParameter(parameter);
        }

        public virtual Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return Collection.Find(predicate).FirstOrDefaultAsync();
        }

        public virtual Task<T> GetByIdAsync(TKey id)
        {
            return Collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            var options = new InsertOneOptions {BypassDocumentValidation = false};
            await Collection.InsertOneAsync(entity, options);
        }

        public virtual async Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            var options = new BulkWriteOptions {IsOrdered = false, BypassDocumentValidation = false};
            return (await Collection.BulkWriteAsync((IEnumerable<WriteModel<T>>) entities, options)).IsAcknowledged;
        }

        public virtual async Task<T> UpdateAsync(TKey id, T entity)
        {
            return await Collection.FindOneAndReplaceAsync(x => x.Id.Equals(id), entity);
        }

        public virtual async Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> predicate)
        {
            return await Collection.FindOneAndReplaceAsync(predicate, entity);
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            return await Collection.FindOneAndDeleteAsync(x => x.Id.Equals(entity.Id));
        }

        public virtual async Task<T> DeleteAsync(TKey id)
        {
            return await Collection.FindOneAndDeleteAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<T> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            return await Collection.FindOneAndDeleteAsync(predicate);
        }

        public virtual Task<int> CountAsync()
        {
            return Collection.AsQueryable().CountAsync();
        }

        public async Task<IResult> Status()
        {
            var isConnect = MongoDatabase.RunCommandAsync((Command<BsonDocument>) "{ping:1}").Wait(1000);
            return isConnect ? new Result(HttpStatusCode.OK) : new Result(HttpStatusCode.InternalServerError);
        }
    }
}