using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Options>(
    builder.Configuration.GetSection("Options"));

var options = new Options();
builder.Configuration.GetSection("Options").Bind(options);

builder.Services.AddOpenTelemetryTracing(b =>
{
    b.AddConsoleExporter()
    .AddSource(options.ServiceName)
    .SetResourceBuilder(ResourceBuilder.CreateDefault()
        .AddService(options.ServiceName, options.ServiceVersion))
    .AddHttpClientInstrumentation()
    .AddAspNetCoreInstrumentation()
    .AddElasticsearchClientInstrumentation()
    .AddOtlpExporter(opt =>
    {
        opt.Endpoint = new Uri(options.OtelUrl);
        opt.ExportProcessorType = ExportProcessorType.Simple;
    });
});

builder.Services.AddHttpClient();

var app = builder.Build();

app.MapGet("/posts/{id}", async (IHttpClientFactory factory, int id) =>
{
    var client = factory.CreateClient();
    var response = await client.GetFromJsonAsync<Post>($"{options.UrlClient}/posts/{id}");
    await Task.Delay(new Random().Next(100, 2000));
    return response;
});

app.Run();