using LoggingInNet;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

var resource = ResourceBuilder.CreateDefault().AddService("CodeWithStu");
var builder = WebApplication.CreateBuilder(args);


builder.Logging
    .AddFilter<OpenTelemetryLoggerProvider>("*", LogLevel.Warning)
    .AddOpenTelemetry(builder =>
    {
        builder
            .SetResourceBuilder(resource)
            .AddConsoleExporter()
            .AddProcessor(new LogProcessor());

        builder.IncludeFormattedMessage = true;
        builder.ParseStateValues = true;
    });


var app = builder.Build();

app.MapGet("/", () =>
{
    return "Hello world!";
});

app.Map("/warning", (ILogger<Program> logger) =>
{
    logger.LogWarning("Test Warning");
    return "[LogWarning] Hello world!";
});

app.Run();
