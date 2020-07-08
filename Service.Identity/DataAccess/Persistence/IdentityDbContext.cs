using Library.DataAccess.MongoDb;
using Library.Utilities.AppSettings;

namespace Service.Identity.DataAccess.Persistence
{
    public class IdentityDbContext : MongoDbContextBase
    {
        public IdentityDbContext(IdentityMongoDbSettings mongoDbSettings) : base(mongoDbSettings)
        {
        }
    }
}