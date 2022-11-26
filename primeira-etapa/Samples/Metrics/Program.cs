using System.Diagnostics.Metrics;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var resource = ResourceBuilder.CreateDefault().AddService("CodeWitStu");
var meterName = "CodeWitStu";
var codeWithStuMeter = new Meter(meterName, "1.0.0");
var requestCounter = codeWithStuMeter.CreateCounter<long>("Requests");
var requestHistogram = codeWithStuMeter.CreateHistogram<long>("RequestP");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenTelemetryMetrics(options =>
{
    options.SetResourceBuilder(resource);
    options.AddConsoleExporter();
    options.AddMeter(meterName);
    options.AddView(instrument =>
    {
        if (instrument.Name == "Requests")
            return new MetricStreamConfiguration { Name = "HttpRequests" };

        if (instrument.Name == "RequestPercentiles")
            return MetricStreamConfiguration.Drop;

        return null;
    });
    options.AddRuntimeInstrumentation(); //old: options.AddRuntimeMetrics()
});

var app = builder.Build();

app.MapGet("/", () =>
{
    requestCounter.Add(1);
    requestHistogram.Record(new Random().Next(0, 100));
    return "Hello World!";
});

app.Run();
