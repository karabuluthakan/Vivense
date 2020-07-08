using System.Threading.Tasks;
using Library.Utilities.Results.Abstract;

namespace Library.CrossCuttingConcerns.HealthChecks.Abstract
{
    public interface IMongoDbHealthChecker
    {
        Task<IResult> GetMongoDbStatus();
    }
}