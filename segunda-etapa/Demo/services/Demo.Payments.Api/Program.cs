using Amazon.DynamoDBv2;
using Demo.OpenTelemetry.Config;
using Demo.Payments.Api.Config;
using Demo.Payments.Api.Infra.DbContext.DynamoDb.Tables;
using Demo.Payments.Api.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddOpenTelemetry();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddOptions<PaymentProviderOptions>(builder.Configuration);
builder.Services.AddMassTransit(builder.Configuration);

var awsOptions = builder.Configuration.GetAWSOptions();
builder.Services.AddInfra(builder.Configuration, builder.Environment, options =>
{
    options.Credentials = awsOptions.Credentials;
    options.Region = awsOptions.Region;
    options.BillingMode = BillingMode.PAY_PER_REQUEST;
    options.TableName = TableNames.PAYMENTS_TABLE;
});

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

app.InitDynamoDb();
app.Run();

