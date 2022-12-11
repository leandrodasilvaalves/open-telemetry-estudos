using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Demo.ProductCatalog.Api.Models
{
    public class Product : ModelBase
    {
        [BsonElement("name")]
        public string Name { get; set; }


        [BsonElement("salePrice")]
        public float SalePrice { get; set; }


        [BsonElement("quantityInStock")]
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