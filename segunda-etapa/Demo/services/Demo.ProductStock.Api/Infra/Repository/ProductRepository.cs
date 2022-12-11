using Demo.ProductStock.Api.Infra.Context;
using Demo.ProductStock.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.ProductStock.Api.Infra.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetAsync(Guid id);
        Task<Product> InsertAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _db;

        public ProductRepository(ProductDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task DeleteAsync(Product product)
        {
            _db.Remove(product);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            /*TODO: aplicar filtro e paginação 
                    para não retornar todos registros da base
            */
            return await _db.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _db.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> InsertAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _db.Update(product);
            await _db.SaveChangesAsync();
            return product;
        }
    }
}