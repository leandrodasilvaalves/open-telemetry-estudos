using System.Diagnostics;
using Microsoft.OpenApi.Models;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "APISaudacao", Description = "Teste com Minimal APIs", Version = "v1" });
});

builder.Services.Configure<Options>(
    builder.Configuration.GetSection("Options"));

var options = new Options();
builder.Configuration.GetSection("Options").Bind(options);


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

builder.Services.AddHttpClient();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "APISaudacao v1");
});

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

app.Run();