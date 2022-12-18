using System.Text.Json;
using Demo.Emails.Worker.Infra;
using Demo.Emails.Worker.Models;
using Demo.SharedModel.Contracts.Events.LogisticProvider;
using MassTransit;

namespace Demo.Emails.Worker.Consumer
{
    public class LogisticProviderWasNotifiedConsumer : IConsumer<ILogisticProviderWasNotifiedEvent>
    {
        private readonly IEmailSenderProvider _emailSenderProvider;

        public LogisticProviderWasNotifiedConsumer(IEmailSenderProvider emailSenderProvider)
        {
            _emailSenderProvider = emailSenderProvider ?? throw new ArgumentNullException(nameof(emailSenderProvider));
        }

        public async Task Consume(ConsumeContext<ILogisticProviderWasNotifiedEvent> context)
        {
            var notification = context.Message.Data;
            var emailBody = JsonSerializer.Serialize(notification, new JsonSerializerOptions { WriteIndented = true });            
            var email = new Email(notification.Customer?.Email, "Good news! Your order is incoming.", emailBody);
            await _emailSenderProvider.SendAsync(email);
        }
    }
}