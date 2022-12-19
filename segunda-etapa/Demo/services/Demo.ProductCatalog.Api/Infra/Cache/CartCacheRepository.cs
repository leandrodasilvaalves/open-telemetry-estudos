using Demo.SharedModel.Models;

namespace Demo.ProductCatalog.Api.Infra.Cache
{
    public interface ICartCacheRepository
    {
        Task<Cart> GetAsync(Guid id);
        Task InsertAsync(Cart cart);
        Task UpdateAsync(Cart cart);
        Task DeleteAsync(Guid id);
    }

    public class CartCacheRepository : ICartCacheRepository
    {
        private readonly ICache<Cart> _cache;

        public CartCacheRepository(ICache<Cart> cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task DeleteAsync(Guid id) => await _cache.RemoveAsync(id.ToString());

        public async Task<Cart> GetAsync(Guid id) => await _cache.GetAsync(id.ToString());

        public async Task InsertAsync(Cart cart) => await _cache.SaveAsync(cart.Id.ToString(), cart);

        public async Task UpdateAsync(Cart cart) => await _cache.UpdateAsync(cart.Id.ToString(), cart);
    }
}