using Demo.ProductStock.Api.Config;
using Demo.ProductStock.Api.Infra.Context;
using Demo.ProductStock.Api.Infra.Providers;
using Demo.ProductStock.Api.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Demo.SharedModel.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductDbContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMassTransit(builder.Configuration);

builder.Services.AddOptions<LogisticProviderOptions>(builder.Configuration);
builder.Services.AddSingleton<IExternalLogisticProvider, ExternalLogisticProvider>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

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
