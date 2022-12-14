namespace Demo.SharedModel.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float SalePrice { get; set; }
        public float CostPrice { get; set; }
        public int QuantityInStock { get; set; }
    }
}