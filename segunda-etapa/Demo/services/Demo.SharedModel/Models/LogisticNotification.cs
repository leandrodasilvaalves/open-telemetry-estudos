using System.Text.Json.Serialization;

namespace Demo.SharedModel.Models
{
    public class LogisticNotification
    {
        [JsonConstructor]
        public LogisticNotification(){}

        public LogisticNotification(Customer customer, List<CartItem> itens)
        {
            Customer = customer;
            Itens = itens;
        }

        public Customer Customer { get; set; }
        public List<CartItem> Itens { get; set; }
    }
}