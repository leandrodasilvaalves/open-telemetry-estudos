using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var serviceName = "Lele.Servico.Exemplo";
var serviceVersion = "v1.0.0";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenTelemetryTracing(b =>
{
    b.AddConsoleExporter()
    .AddSource(serviceName)
    .SetResourceBuilder(ResourceBuilder.CreateDefault()
        .AddService(serviceName, serviceVersion))
    .AddHttpClientInstrumentation()
    .AddAspNetCoreInstrumentation()
    .AddOtlpExporter(opt =>
    {
        opt.Endpoint = new Uri("http://otel-collector:4317");
        opt.ExportProcessorType = ExportProcessorType.Simple;
    });
});

var urlService = builder.Configuration["UrlService"];

var app = builder.Build();

var httpClient = new HttpClient();
app.MapGet("/posts/{id}", async (int id) =>
{
    var url = $"{urlService}/posts/{id}";
    Console.WriteLine("[url]: " + url);
    var response = await httpClient.GetFromJsonAsync<Post>(url);
    await Task.Delay(new Random().Next(100, 2000));
    return response;
});

app.Run();
