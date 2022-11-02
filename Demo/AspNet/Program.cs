using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

var options = builder.Services
    .AddOptions(builder.Configuration)
    .GetOptions(builder.Configuration);

builder.Services
    .AddEndpointsApi()
    .AddRepositories(options)
    .AddClients()
    .AddServices()
    .AddSwagger(options);

builder.Services.AddOpenTelemetryTracing(providerBuilder =>
{
    providerBuilder
        .AddConsoleExporter()
        .AddSource(options.ServiceName)
        .SetResourceBuilder(ResourceBuilder.CreateDefault()
            .AddService(options.ServiceName, options.ServiceVersion))
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation()
        .AddRedisInstrumentation()
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