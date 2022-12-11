using Demo.ProductCatalog.Api.Config;
using Demo.ProductCatalog.Api.Models;
using Microsoft.Extensions.Options;

namespace Demo.ProductCatalog.Api.Infra.Repository
{
    public interface IProductRepository : IRepositoryBase<Product> { }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {

        public ProductRepository(IOptions<MongoConfig> options)
            : base(options)
        {
        }

        protected override string CollectionName => "products";
    }
}