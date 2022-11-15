using System.Diagnostics;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

Activity.DefaultIdFormat = ActivityIdFormat.W3C;
builder.Services.AddHttpClient("ProjectB")
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5002"));

builder.Services.AddOpenTelemetryTracing(builder =>
{
    builder
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddJaegerExporter();
});


var app = builder.Build();

app.MapGet("/", async context =>
{
    context.Response.Headers.Add("Request-Id", Activity.Current?.TraceId.ToString() ?? string.Empty);
    using var client = context.RequestServices.GetRequiredService<IHttpClientFactory>().CreateClient("ProjectB");
    var content = client.GetStringAsync("/");

    await context.Response.WriteAsync("Hello from Project A \n");
    await context.Response.WriteAsync(await content);
});

app.Run();
