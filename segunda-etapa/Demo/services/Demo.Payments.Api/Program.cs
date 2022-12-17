using Demo.Payments.Api.Config;
using Demo.Payments.Api.Infra;
using Demo.SharedModel.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<PaymentProviderOptions>(builder.Configuration);
builder.Services.AddMassTransit(builder.Configuration);
builder.Services.AddSingleton<IPaymentProvider, ExternalPaymentProvider>();
builder.Services.AddSingleton<IPaymentRepository, PaymentRepository>();

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
