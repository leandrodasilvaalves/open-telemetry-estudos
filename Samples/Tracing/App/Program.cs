using App;
using Library;
using OpenTelemetry.Trace;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<ServiceA>();        

        services.AddOpenTelemetryTracing(builder =>
        {
            builder.AddConsoleExporter()
                .SetSampler(new AlwaysOnSampler())
                .AddSource(MyActivitySource.Name);
        });
    })
    .Build();

await host.RunAsync();
