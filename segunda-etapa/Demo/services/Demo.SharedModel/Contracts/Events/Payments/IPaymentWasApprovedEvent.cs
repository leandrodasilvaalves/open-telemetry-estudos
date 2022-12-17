using Demo.SharedModel.Models;

namespace Demo.SharedModel.Contracts.Events.Payments
{
    public interface IPaymentWasApprovedEvent : IEventBase<Payment>
    {        
    }
}