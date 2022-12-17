using Demo.SharedModel.Models;

namespace Demo.SharedModel.Contracts.Events.Payments
{
    public interface IPaymentWasRejectedEvent : IEventBase<Payment>
    {        
    }
}