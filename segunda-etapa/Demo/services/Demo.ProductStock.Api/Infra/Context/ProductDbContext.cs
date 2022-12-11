using Demo.SharedModel.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.ProductStock.Api.Infra.Context
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) 
            : base(options){}

        public DbSet<Product> Products { get; set; }
    }
}