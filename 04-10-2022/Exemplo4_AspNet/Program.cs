
using Exemplo4_AspNet;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

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
    .AddJaegerExporter(jgOpt =>{
        jgOpt.AgentHost = "localhost";
        jgOpt.AgentPort = 6831;
    });

});

var app = builder.Build();

var httpClient = new HttpClient();
app.MapGet("/posts", async () => {
    var response = await httpClient
        .GetFromJsonAsync<Post>("https://jsonplaceholder.typicode.com/posts/1");
    return response;
});

app.Run();
