using System.Threading.Tasks;
using Library.CrossCuttingConcerns.HealthChecks.Abstract;
using Library.Utilities.Results.Abstract;

namespace Library.CrossCuttingConcerns.HealthChecks
{
    public class MongoDbHealthChecker : IMongoDbHealthChecker
    {
        private readonly IHealthCheckService healthCheck;

        public MongoDbHealthChecker(IHealthCheckService healthCheck)
        {
            this.healthCheck = healthCheck;
        }

        public async Task<IResult> GetMongoDbStatus()
        {
            var isConnect = await healthCheck.Status();
            return await Task.FromResult(isConnect);
        }
    }
}