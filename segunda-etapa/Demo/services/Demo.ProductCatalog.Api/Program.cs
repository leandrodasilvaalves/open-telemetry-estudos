using Demo.ProductCatalog.Api.Config;
using Demo.ProductCatalog.Api.Infra.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoConfig>(builder.Configuration.GetSection("MongoConfig"));
builder.Services.AddSingleton<IProductRepository, ProductRepository>();

builder.Services.AddMassTransit(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
