using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
var builder = WebApplication.CreateBuilder(args);

var options = builder.Services
    .AddOptions(builder.Configuration)
    .GetOptions(builder.Configuration);

var resource = ResourceBuilder.CreateDefault()
            .AddService(options.ServiceName, options.ServiceVersion);

builder.Services
    .AddEndpointsApi()
    .AddMassTransit(options.Rabbit)
    .AddRepositories(options)
    .AddClients()
    .AddServices()
    .AddSwagger(options);

builder.Logging
    .AddFilter<OpenTelemetryLoggerProvider>("*", LogLevel.Warning)
    .AddOpenTelemetry(providerBuilder =>
    {
        providerBuilder.SetResourceBuilder(resource);
        providerBuilder.AddConsoleExporter();
        providerBuilder.AddOtlpExporter(opt =>
        {
            opt.Endpoint = new Uri(options.OtelUrl);
            opt.ExportProcessorType = ExportProcessorType.Simple;
        });
    });

builder.Services.AddOpenTelemetryTracing(providerBuilder =>
{
    providerBuilder
        .AddConsoleExporter()
        .AddSource(options.ServiceName)
        .SetResourceBuilder(resource)
        // .AddRedisInstrumentation()
        .AddMassTransitInstrumentation()
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation()
        .AddOtlpExporter(opt =>
        {
            opt.Endpoint = new Uri(options.OtelUrl);
            opt.ExportProcessorType = ExportProcessorType.Simple;
        });
});

var app = builder.Build();
app.ConfigureSwagger(options);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.InitAsync();
await app.RunAsync();