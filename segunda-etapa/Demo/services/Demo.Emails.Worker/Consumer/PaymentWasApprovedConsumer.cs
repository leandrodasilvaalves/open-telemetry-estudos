using Demo.Emails.Worker.Infra;
using Demo.Emails.Worker.Models;
using Demo.SharedModel.Contracts.Events.Payments;
using MassTransit;
using System.Text.Json;

namespace Demo.Emails.Worker.Consumer
{
    public class PaymentWasApprovedConsumer : IConsumer<IPaymentWasApprovedEvent>
    {
        private readonly IEmailSenderProvider _emailSenderProvider;

        public PaymentWasApprovedConsumer(IEmailSenderProvider emailSenderProvider)
        {
            _emailSenderProvider = emailSenderProvider ?? throw new ArgumentNullException(nameof(emailSenderProvider));
        }

        public async Task Consume(ConsumeContext<IPaymentWasApprovedEvent> context)
        {
            var payment = context.Message.Data;
            var emailBody = JsonSerializer.Serialize(payment, new JsonSerializerOptions { WriteIndented = true });            
            var email = new Email(payment.Customer?.Email, "Your payment was approved", emailBody);
            await _emailSenderProvider.SendAsync(email);
        }
    }
}