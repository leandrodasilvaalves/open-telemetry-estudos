namespace Demo.ProductCatalog.Api.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}