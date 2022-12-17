using Demo.SharedModel.Models;

namespace Demo.Payments.Api.Infra
{
    public interface IPaymentRepository
    {
        public Task SaveAsync(Payment payment);
    }

    public class PaymentRepository : IPaymentRepository
    {
        public Task SaveAsync(Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}