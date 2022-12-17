namespace Demo.SharedModel.Models
{
    public class Payment
    {
        public Payment(Customer customer, float amount, Guid cartId)
        {
            Customer = customer;
            Amount = amount;
            CartId = cartId;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        public float Amount { get; set; }
        public Guid CartId { get; set; }
    }
}