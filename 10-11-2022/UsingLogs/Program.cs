using System.Text.Json;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using UsingLogs;

var resource = ResourceBuilder.CreateDefault().AddService("CodeWithStu");

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddFilter<OpenTelemetryLoggerProvider>("*", LogLevel.Warning);
builder.Logging.AddOpenTelemetry(options =>
{
    options.SetResourceBuilder(resource);
    options.AddConsoleExporter();
    options.AddProcessor(new LogProcessor());
});
var app = builder.Build();

app.Map("/json", (ILogger<Program> logger) =>
{
    var json = JsonSerializer.Serialize(new { Teste = "teste", Warning = "warning" });
    logger.LogWarning(json);
    return "[json] Hello world!";
});

app.Map("/warning", (ILogger<Program> logger) =>
{
    logger.LogWarning("Test Warning");
    return "[LogWarning] Hello world!";
});

app.Map("/info", (ILogger<Program> logger) =>
{

    logger.LogInformation("Test Info");
    return "[LogInformation] Hello world!";
});

app.Map("/error", (ILogger<Program> logger) =>
{

    logger.LogError("Test Error");
    return "[LogError] Hello world!";
});

app.Map("/critical", (ILogger<Program> logger) =>
{

    logger.LogCritical("Test Critical");
    return "[LogCritical] Hello world!";
});


app.Run();