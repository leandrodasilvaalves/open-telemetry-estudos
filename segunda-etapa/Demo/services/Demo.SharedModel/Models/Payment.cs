using System.Text.Json.Serialization;

namespace Demo.SharedModel.Models
{
    public class Payment
    {
        [JsonConstructor]
        public Payment(){}
        
        public Payment(Customer customer, float amount, Cart cart)
        {
            Customer = customer;
            Amount = amount;
            Items = cart.Items;            
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        public float Amount { get; set; }        
        public List<CartItem> Items {get; set;}
    }
}