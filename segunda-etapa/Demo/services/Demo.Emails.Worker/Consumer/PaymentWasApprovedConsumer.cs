using Demo.SharedModel.Contracts.Events.Payments;
using MassTransit;
using System.Text.Json;

namespace Demo.Emails.Worker.Consumer
{
    public class PaymentWasApprovedConsumer : IConsumer<IPaymentWasApprovedEvent>
    {
        public Task Consume(ConsumeContext<IPaymentWasApprovedEvent> context)
        {
            var msg = context.Message.Data;
            var json = JsonSerializer.Serialize(msg, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine("payment received: {0}", json);
            return Task.CompletedTask;
        }
    }
}