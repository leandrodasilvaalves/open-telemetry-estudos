namespace Demo.ProductCatalog.Api.Models
{
    public class Cart
    {

        public Cart()
        {
            Items = new List<CartItem>();
            Status = CartStatus.OPEN;
        }

        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        public List<CartItem> Items {get; set;}

        public float Total => Items.Sum(i => i.TotalPrice);
        public CartStatus Status { get; set; }

        public void AddOrUpdate(CartItem item)
        {
            var oldItem = Items.FirstOrDefault(c => c.ProductId == item.ProductId);
            if (oldItem is not null)
                Items.Remove(oldItem);

            Items.Add(item);
        }

        public void Remove(string productId)
        {
            var item = Items.FirstOrDefault(c => c.ProductId == productId);
            Items.Remove(item);
        }

        public void Close() => Status = CartStatus.CLOSED;
        public void WaitPayment() => Status = CartStatus.WAITING_PAYMENT;
    }

    public class CartItem
    {
        public int Quantity { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public float UnitPrice { get; set; }
        public float TotalPrice => UnitPrice * Quantity;
    }

    public enum CartStatus
    {
        OPEN,
        WAITING_PAYMENT,
        CLOSED
    }
}