using Demo.ProductCatalog.Api.Config;
using Demo.ProductCatalog.Api.Infra.Cache;
using Demo.ProductCatalog.Api.Infra.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<MongoConfig>(builder.Configuration);
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton(typeof(ICache<>), typeof(Redis<>));
builder.Services.AddSingleton<ICartRepository, CartRepository>();

builder.Services.AddMassTransit(builder.Configuration);
builder.Services.AddRedids(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
