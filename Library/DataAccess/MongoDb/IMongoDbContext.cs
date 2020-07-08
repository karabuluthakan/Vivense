using System;
using System.Threading.Tasks; 
using MongoDB.Driver;

namespace Library.DataAccess.MongoDb
{
    public interface IMongoDbContext : IDisposable
    {
        void AddCommand(Func<Task> func);
        Task<int> SaveChanges();
        IMongoDatabase MongoDatabase { get; }
    }
}