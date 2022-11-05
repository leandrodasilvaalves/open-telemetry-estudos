using System.Reflection;
using MassTransit;
using Microsoft.OpenApi.Models;

internal static class CustomExtensions
{
    internal static IServiceCollection AddEndpointsApi(this IServiceCollection self)
    {
        self.AddControllers();
        self.AddEndpointsApiExplorer();
        return self;
    }

    internal static IServiceCollection AddMassTransit(this IServiceCollection self, RabbitOptions options)
    {
        self.AddMassTransit(config =>
        {
            config.AddConsumers(Assembly.GetEntryAssembly());
            config.SetKebabCaseEndpointNameFormatter();
            config.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(options.ConnectionString), h =>
                {
                    h.Username(options.UserName);
                    h.Password(options.Password);
                });
                cfg.ReceiveEndpoint("post-was-consulted", ep =>
                {
                    ep.PrefetchCount = 1;
                    ep.UseMessageRetry(r => r.Interval(2, 100));
                    ep.ConfigureConsumer<PostWasConsultedConsumer>(provider);
                });
            }));
        });
        return self;
    }

    internal static IServiceCollection AddOptions(this IServiceCollection self, IConfiguration configuration)
    {
        self.Configure<Options>(configuration.GetSection("Options"));
        return self;
    }

    internal static Options GetOptions(this IServiceCollection self, IConfiguration configuration)
    {
        var options = new Options();
        configuration.GetSection("Options").Bind(options);
        return options;
    }

    internal static IServiceCollection AddClients(this IServiceCollection self)
    {
        self.AddHttpClient();
        self.AddSingleton<IPostClient, PostClient>();
        return self;
    }


    internal static IServiceCollection AddRepositories(this IServiceCollection self, Options options)
    {
        self.AddSingleton<IPostRepository, PostRepository>();

        if (options.UseCache)
        {
            self.AddDistributedRedisCache(opt =>
            {
                opt.Configuration = options.Redis.ConnectionString;
                opt.InstanceName = options.Redis.InstanceName;
            });
            self.Decorate<IPostRepository, CacheRepository>();
        }

        self.AddAsyncInitializer<Seed>();
        return self;
    }

    internal static IServiceCollection AddServices(this IServiceCollection self)
    {
        self.AddSingleton<IPostService, PostService>();
        return self;
    }

    internal static IServiceCollection AddSwagger(this IServiceCollection self, Options options)
    {
        self.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = options.ServiceName, Description = "Demo Opentelemetry", Version = "v1" });
        });
        return self;
    }

    internal static void ConfigureSwagger(this WebApplication app, Options options)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{options.ServiceName} - Opentelemetry - v1");
        });
    }

}