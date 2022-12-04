using System.Diagnostics;
using System.Diagnostics.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Exporter;

var serviceName = "Lele.Something.Api";
var serviceVersion = "1.0.0";
var MyActivitySource = new ActivitySource(serviceName);

var builder = WebApplication.CreateBuilder(args);

var appResourceBuilder = ResourceBuilder.CreateDefault()
    .AddService(serviceName: serviceName, serviceVersion: serviceVersion);

builder.Services.AddOpenTelemetryTracing(options =>
{
    options
        .AddConsoleExporter()
        .AddOtlpExporter(opt => {
            opt.Protocol = OtlpExportProtocol.HttpProtobuf;
        })
        .AddSource(MyActivitySource.Name)
        .SetResourceBuilder(appResourceBuilder)
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation();
});

var meter = new Meter(serviceName);
var counter = meter.CreateCounter<long>("app.request-counter");
builder.Services.AddOpenTelemetryMetrics(options =>
{
    options
        .AddConsoleExporter()
        .AddOtlpExporter(opt => {
            opt.Protocol = OtlpExportProtocol.HttpProtobuf;
        })
        .AddMeter(meter.Name)
        .SetResourceBuilder(appResourceBuilder)
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation();
});

var app = builder.Build();

app.MapGet("/", () =>
{
    using var activity = MyActivitySource.StartActivity("SayHello");
    activity?.SetTag("foo", 1);
    activity?.SetTag("bar", "Hello, World!");
    activity?.SetTag("baz", new int[] { 1, 2, 3 });   
    
    return "Hello, World!";
});

app.Run();
