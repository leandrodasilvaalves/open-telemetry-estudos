using Exemplo4_AspNet;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using System.Diagnostics.Metrics;

var serviceName = "MyCompany.MyProduct.MyService";
var serviceVersion = "1.0.0";


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenTelemetryTracing(b =>
{
    b.AddConsoleExporter()
    .AddSource(serviceName)
    .SetResourceBuilder(
        ResourceBuilder.CreateDefault()
            .AddService(serviceName: serviceName, serviceVersion))
    .AddHttpClientInstrumentation()
    .AddAspNetCoreInstrumentation()
    .AddJaegerExporter(jgOpt =>
    {
        jgOpt.AgentHost = "localhost";
        jgOpt.AgentPort = 6831;
    })
    .AddZipkinExporter(o =>
    {
        o.Endpoint = new Uri("http://localhost:9412/api/v2/spans");
    });
});


var s_meter = new Meter("HatCo.HatStore", "1.0.0");
Counter<int> s_hatsSold = s_meter.CreateCounter<int>(
    name: "hats-sold",
    unit: "Hats",
    description: "The number of hats sold in our store"
);
builder.Services.AddOpenTelemetryMetrics(b =>
{
    b
    .AddConsoleExporter()    
    .AddPrometheusExporter(options =>
    {
        options.StartHttpListener = true;
        options.HttpListenerPrefixes = new string[] { "http://localhost:9090/" };
    });
});

var app = builder.Build();

var httpClient = new HttpClient();
app.MapGet("/posts", async () =>
{
    var response = await httpClient
        .GetFromJsonAsync<Post>("https://jsonplaceholder.typicode.com/posts/1");
    return response;
});

app.Run();
