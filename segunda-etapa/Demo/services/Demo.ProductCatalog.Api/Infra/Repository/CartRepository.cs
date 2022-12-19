using Demo.ProductCatalog.Api.Config;
using Demo.ProductCatalog.Api.Models;
using Microsoft.Extensions.Options;

namespace Demo.ProductCatalog.Api.Infra.Repository
{
    public interface ICartRepository : IRepositoryBase<Cart> { }

    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(IOptions<MongoConfig> options) 
            : base(options)
        {
        }

        protected override string CollectionName => "carts";
    }
}