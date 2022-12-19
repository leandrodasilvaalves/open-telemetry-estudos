using Demo.SharedModel.Models;

namespace Demo.ProductCatalog.Api.Models
{
    public class Cart : ModelBase
    {
        public Customer Customer { get; set; }
        public List<CartItem> Items { get; set; }
        public float Total => Items.Sum(i => i.TotalPrice);
        public CartStatus Status { get; set; }
        public Guid PaymentId { get; set; }
        public Guid CartId { get; set; }
    }
}