using Demo.SharedModel.Contracts.Events.Carts;
using MassTransit;
using System.Text.Json;

namespace Demo.Payment.Api.Consumers
{
    public class CartWasCheckoutedConsumer : IConsumer<ICartWasCheckouted>
    {
        public Task Consume(ConsumeContext<ICartWasCheckouted> context)
        {
            var cart = context.Message?.Data;
            Console.WriteLine("Received Message: {0}", JsonSerializer.Serialize(cart, new JsonSerializerOptions { WriteIndented = true }));
            return Task.CompletedTask;
        }
    }
}
