using System.Diagnostics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var serviceName = "MyCompany.MyProduct.MyService";
var serviceVersion = "1.0.0";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenTelemetryTracing(tracerProviderBuilder =>
{
    tracerProviderBuilder
        .AddConsoleExporter()
        .AddOtlpExporter(opt =>
        {
            opt.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
        })
        .AddSource(serviceName)
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName, serviceVersion))
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation()
        .AddSqlClientInstrumentation();
});


var app = builder.Build();

var myActivitySource = new ActivitySource(serviceName);

app.MapGet("/hello", () =>
{

    var activity = myActivitySource.StartActivity("SayHello");
    activity?.SetTag("foo", 1);
    activity?.SetTag("bar", "Hello, World!");
    activity?.SetTag("baz", new int[] { 1, 2, 3 });

    return "Hello World!";
});

app.Run();
