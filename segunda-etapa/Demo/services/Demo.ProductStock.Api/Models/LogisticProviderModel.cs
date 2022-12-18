namespace Demo.ProductStock.Api.Models
{
    public class LogisticProviderModel
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public bool Received { get; set; }
    }
}