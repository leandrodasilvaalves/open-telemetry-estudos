using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Text.Json;
using System.Text;
using Exemplo5_Aspnet_ELK;

var builder = WebApplication.CreateBuilder(args);

var options = builder.Services.GetOptions(builder.Configuration);
builder.Services
    .AddEndpointsApiExplorer()
    .AddRepositories()
    .AddClients()
    .AddSwagger(options);

builder.Services.AddOpenTelemetryTracing(providerBuilder =>
{
    providerBuilder.AddConsoleExporter()
    .AddSource(options.ServiceName)
    .SetResourceBuilder(ResourceBuilder.CreateDefault()
        .AddService(options.ServiceName, options.ServiceVersion))
    .AddHttpClientInstrumentation()
    .AddAspNetCoreInstrumentation()
    .AddElasticsearchClientInstrumentation()
    .AddJaegerExporter(opt =>
    {
        opt.AgentHost = options.Jaeger.Url;
        opt.AgentPort = options.Jaeger.Port;
    })
    .AddZipkinExporter(opt => opt.Endpoint = options.Zipkin.Uri)
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
app.MapGet("/posts/{id}", async (IHttpClientFactory factory, int id) =>
{
    var activity = source.StartActivity($"GET /posts/{id}");

    var client = factory.CreateClient();
    activity.SetTag("created_client", client.MaxResponseContentBufferSize);

    activity.SetTag("before_request", $"post_id:{id}");
    var response = await client.GetFromJsonAsync<Post>($"{options.UrlClient}/posts/{id}");
    await Task.Delay(new Random().Next(100, 2000));
    activity.SetTag("after_request", $"post_id:{id}");

    return response;
});

app.MapPost("/posts", async (IHttpClientFactory factory, Post post) =>
{
    var activity = source.StartActivity($"POST /posts");

    var client = factory.CreateClient();
    activity.SetTag("created_client", client.MaxResponseContentBufferSize);

    activity.SetTag("before_request", $"post_id:{post.Id}");

    var json = JsonSerializer.Serialize(post);
    var response = await client.PostAsync($"{options.UrlClient}/posts", new StringContent(json, Encoding.UTF8, "application/json"));
    await Task.Delay(new Random().Next(100, 2000));

    activity.SetTag("after_request", $"post_id:{post.Id}");

    return Results.Ok(post);
});
#endregion

app.Run();