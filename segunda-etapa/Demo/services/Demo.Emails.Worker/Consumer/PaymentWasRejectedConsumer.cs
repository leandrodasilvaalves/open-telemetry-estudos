using System.Text.Json;
using Demo.Emails.Worker.Infra;
using Demo.Emails.Worker.Models;
using Demo.SharedModel.Contracts.Events.Payments;
using MassTransit;

namespace Demo.Emails.Worker.Consumer
{
    public class PaymentWasRejectedConsumer : IConsumer<IPaymentWasRejectedEvent>
    {
        private readonly IEmailSenderProvider _emailSenderProvider;

        public PaymentWasRejectedConsumer(IEmailSenderProvider emailSenderProvider)
        {
            _emailSenderProvider = emailSenderProvider ?? throw new ArgumentNullException(nameof(emailSenderProvider));
        }

        public async Task Consume(ConsumeContext<IPaymentWasRejectedEvent> context)
        {
            var payment = context.Message.Data;
            var emailBody = JsonSerializer.Serialize(payment, new JsonSerializerOptions { WriteIndented = true });            
            var email = new Email(payment.Customer?.Email, "I'm so sorry! Your payment was rejected", emailBody);
            await _emailSenderProvider.SendAsync(email);
        }
    }
}