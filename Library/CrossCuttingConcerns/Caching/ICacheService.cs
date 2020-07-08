using System.Threading.Tasks;

namespace Library.CrossCuttingConcerns.Caching
{
    public interface ICacheService
    {
        Task<T> Get<T>(string key);
        Task<string> Get(string key);
        Task Add(string key, object data, int duration = 90);
        Task<bool> IsAdd(string key);
        Task Remove(string key);
        Task RemoveByPattern(string pattern);
    }
}