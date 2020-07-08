using System;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Library.Models.Abstract;
using Library.Utilities.QueryParameters;
using MongoDB.Driver.Linq;

namespace Library.Extensions
{
    public static class MongoQueryableExtensions
    {
        public static IMongoQueryable<T> QueryParameter<T>(this IMongoQueryable<T> query, QueryParameter parameter)
            where T : class, IEntity, new()
        {
            if (parameter == null)
            {
                return query;
            }

            if (!string.IsNullOrEmpty(parameter.Filter))
            {
                var filter = (Expression<Func<T, bool>>) DynamicExpressionParser.ParseLambda(typeof(T), null, parameter.Filter);
                query = query.Where(filter);
            }

            if (parameter.PageNumber > 1)
            {
                var skip = (parameter.PageNumber - 1) * parameter.PageSize;
                query = query.Skip(skip);
            }

            if (parameter.PageSize > 0)
            {
                query = query.Take(parameter.PageSize);
            }
            
            if (!string.IsNullOrEmpty(parameter.OrderBy))
            {
                query = query.OrderBy(parameter.OrderBy) as IMongoQueryable<T>;
            }

            return query;
        }
    }
}