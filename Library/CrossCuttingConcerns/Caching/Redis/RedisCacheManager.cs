using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Library.CrossCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : ICacheService
    {
        private readonly IDistributedCache cache;

        public RedisCacheManager(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async Task<T> Get<T>(string key)
        {
            var data = await cache.GetStringAsync(key);
            return JsonSerializer.Deserialize<T>(data);
        }

        public async Task<string> Get(string key)
        {
            return await cache.GetStringAsync(key);
        }

        public async Task Add(string key, object data, int duration = 90)
        {
            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(duration)
            };
            await cache.SetAsync(key, JsonSerializer.SerializeToUtf8Bytes(data), options);
        }

        public async Task<bool> IsAdd(string key)
        {
            var data = await cache.GetStringAsync(key);
            return !string.IsNullOrEmpty(data);
        }

        public async Task Remove(string key)
        {
            await cache.RemoveAsync(key);
        }

        public async Task RemoveByPattern(string pattern)
        {
            //Todo:
            await cache.RemoveAsync(pattern);
        }
    }
}