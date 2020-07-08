using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Utilities.AppSettings;
using MongoDB.Driver;

namespace Library.DataAccess.MongoDb
{
    public abstract class MongoDbContextBase : IMongoDbContext
    {
        public IMongoDatabase MongoDatabase { get; private set; }
        protected IClientSessionHandle Session;
        protected MongoClient MongoClient;
        private readonly List<Func<Task>> commands;

        protected MongoDbContextBase(MongoDbSettings mongoDbSettings)
        {
            MongoClientConfigure(mongoDbSettings);
            commands = new List<Func<Task>>();
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func)
        {
            commands.Add(func);
        }

        public async Task<int> SaveChanges()
        {
            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();
                var commandTasks = commands.Select(x => x());
                await Task.WhenAll(commandTasks);
                await Session.CommitTransactionAsync();
            }

            return commands.Count;
        }

        private void MongoClientConfigure(MongoDbSettings mongoDbSettings)
        {
            if (MongoDatabase == null)
            {
                MongoClient = new MongoClient(mongoDbSettings.ConnectionString);
                MongoDatabase = MongoClient.GetDatabase(mongoDbSettings.DefaultDatabase);
            }
        }
    }
}