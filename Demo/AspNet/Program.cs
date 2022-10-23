using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

var options = builder.Services
    .AddOptions(builder.Configuration)
    .GetOptions(builder.Configuration);

builder.Services
    .AddEndpointsApiExplorer()
    .AddRepositories()
    .AddClients()
    .AddServices()
    .AddSwagger(options);

builder.Services.AddOpenTelemetryTracing(providerBuilder =>
{
    providerBuilder
    .AddConsoleExporter()
    .AddSource(options.ServiceName)
    .SetResourceBuilder(ResourceBuilder.CreateDefault()
        .AddService(options.ServiceName, options.ServiceVersion))
    .AddHttpClientInstrumentation()
    .AddAspNetCoreInstrumentation()
    .AddOtlpExporter(opt =>
    {
        opt.Endpoint = new Uri(options.OtelUrl);
        opt.ExportProcessorType = ExportProcessorType.Simple;
    });
});

var app = builder.Build();
app.ConfigureSwagger(options);

#region [endpoints]
var source = new ActivitySource(options.ServiceName);
app.MapGet("/posts", async (IPostService service) =>
{
    var activity = source.StartActivity("GET /posts");
    activity.SetTag("before_request", "get all posts");

    var response = await service.GetAll();
    await Task.Delay(new Random().Next(100, options.MaxDelayMileseconds));

    activity.SetTag("after_request", "get all posts");
    return response;
});

app.MapGet("/posts/{id}", async (IPostService service, Guid id) =>
{
    var activity = source.StartActivity($"GET /posts/{id}");
    activity.SetTag("before_request", $"post_id:{id}");

    var response = await service.GetById(id);
    await Task.Delay(new Random().Next(100, options.MaxDelayMileseconds));

    activity.SetTag("after_request", $"post_id:{id}");
    return response;
});

app.MapPost("/posts", async (IPostService service, Post post) =>
{
    var activity = source.StartActivity($"POST /posts");
    activity.SetTag("before_request", $"post_id:{post.Id}");

    await service.Include(post);
    await Task.Delay(new Random().Next(100, options.MaxDelayMileseconds));

    activity.SetTag("after_request", $"post_id:{post.Id}");
    return Results.Ok();
});
#endregion

await app.InitAsync();
await app.RunAsync();