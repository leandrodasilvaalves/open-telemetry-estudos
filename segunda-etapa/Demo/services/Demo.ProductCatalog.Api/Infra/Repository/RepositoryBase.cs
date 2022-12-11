using Demo.ProductCatalog.Api.Config;
using Demo.ProductCatalog.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Demo.ProductCatalog.Api.Infra.Repository
{
    public interface IRepositoryBase<T> where T : ModelBase
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(string id);
        Task InsertAsync(T model);
        Task UpdateAsync(T model);
        Task DeleteAsync(string id);
    }

    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : ModelBase
    {
        protected abstract string CollectionName { get; }

        public IMongoCollection<T> Collection { get; }

        public RepositoryBase(IOptions<MongoConfig> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
            Collection = mongoDatabase.GetCollection<T>(CollectionName);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await Collection.Find(_ => true).ToListAsync();

        public async Task<T> GetAsync(string id)
            => await Collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task InsertAsync(T model)
            => await Collection.InsertOneAsync(model);

        public async Task UpdateAsync(T model)
            => await Collection.ReplaceOneAsync(x => x.Id == model.Id, model);

        public async Task DeleteAsync(string id)
            => await Collection.DeleteOneAsync(x => x.Id == id);
    }
}