using Demo.Emails.Worker.Config;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        // services.AddHostedService<Worker>();                    
        services.AddMassTransit(host.Configuration);
    })
    .Build();

await host.RunAsync();
