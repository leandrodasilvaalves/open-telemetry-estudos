using Demo.SharedModel.Models;

namespace Demo.ProductStock.Api.Models
{
    public class LogisticNotification
    {
        public Customer Customer { get; set; }
        public List<CartItem> Itens { get; set; }
    }
}