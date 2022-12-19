namespace Demo.ProductCatalog.Api.Models
{
    public class Product : ModelBase
    {
        public Product() : base() { }

        public string Name { get; set; }

        public float SalePrice { get; set; }

        public int QuantityInStock { get; set; }

        public static explicit operator Product(SharedModel.Models.Product product)
        {
            return new Product
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                QuantityInStock = product.QuantityInStock,
                SalePrice = product.SalePrice,
            };
        }
    }
}