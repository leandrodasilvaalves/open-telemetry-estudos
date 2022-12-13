using System.Text.Json;
using Demo.ProductCatalog.Api.Config;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Demo.ProductCatalog.Api.Infra.Cache
{
    public interface ICache<T> where T : class
    {
        Task<T> GetAsync(string id);
        Task RemoveAsync(string key);
        Task SaveAsync(string key, T model);
        Task UpdateAsync(string key, T model);
    }

    public class Redis<T> : ICache<T> where T : class
    {
        private readonly IDistributedCache _database;
        private readonly RedisConfig _options;

        public Redis(IDistributedCache database, IOptions<RedisConfig> options)
        {
            _database = database ?? throw new ArgumentNullException(nameof(database));
            _options = options.Value ?? throw new ArgumentException(nameof(options));
        }

        public async Task<T> GetAsync(string id)
        {
            var model = await _database.GetStringAsync(id);
            return model != null ? JsonSerializer.Deserialize<T>(model) : default;
        }

        public async Task SaveAsync(string key, T model)
        {
            var cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(_options.TTL));
            var json = JsonSerializer.Serialize(model);
            await _database.SetStringAsync(key, json, cacheOptions);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.RemoveAsync(key);
        }

        public async Task UpdateAsync(string key, T model)
        {
            await RemoveAsync(key);
            await SaveAsync(key, model);
        }
    }
}