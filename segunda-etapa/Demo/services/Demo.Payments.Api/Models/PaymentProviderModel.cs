namespace Demo.Payments.Api.Models
{
    public class PaymentProviderModel
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public PaymentProviderStatus Status { get; set; }
    }

    public enum PaymentProviderStatus
    {
        APPROVED,
        REJECTED
    }
}