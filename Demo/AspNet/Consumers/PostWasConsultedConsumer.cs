using AspNet.Domain.Events;
using MassTransit;

public class PostWasConsultedConsumer : IConsumer<PostWasConsulted>
{
    private readonly ILogger<PostWasConsultedConsumer> _logger;

    public PostWasConsultedConsumer(ILogger<PostWasConsultedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PostWasConsulted> context)
    {
        _logger.LogInformation("[This post was consulted]: " + context.Message.ToString());
        await Task.CompletedTask;
    }
}

public class PostWasConsultedConsumerDefinition : ConsumerDefinition<PostWasConsultedConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<PostWasConsultedConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(r => r.Intervals(100, 200, 500, 800, 1000));
        endpointConfigurator.UseInMemoryOutbox();
    }
}