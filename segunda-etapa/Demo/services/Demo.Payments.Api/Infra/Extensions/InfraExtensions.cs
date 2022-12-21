using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Demo.Payments.Api.Infra.DbContext.Contracts;
using Demo.Payments.Api.Infra.DbContext.DynamoDb;
using Demo.Payments.Api.Infra.DbContext.Factories;
using Demo.Payments.Api.Infra.DbContext.Models;
using Demo.Payments.Api.Infra.DbContext.Repositories;
using Demo.SharedModel.Models;

namespace Demo.Payments.Api.Infra.Extensions
{
    public static class InfraExtensions
    {

        public static async void InitDynamoDb(this WebApplication app)
        {
            var dynamoDb = app.Services.GetService<IDatabaseContext>();
            await dynamoDb.ConfigureAsync();
        }

        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment, Action<DynamoDbRepositoryOptions> configure = null)
        {
            var options = new DynamoDbRepositoryOptions();
            configure?.Invoke(options);

            if (string.IsNullOrWhiteSpace(options.TableName))
                throw new InvalidOperationException("Table name cannot be null");

            services.AddSingleton(options);

            if (environment.IsDevelopment())
                services.AddSingleton(configuration.GetAmazonDynamoDB());
            else
                services.AddAWSService<IAmazonDynamoDB>(options);


            services.AddSingleton<IDatabaseContext, DynamoDbDatabaseContext>();
            services.AddSingleton<IDynamoDbWritableRepository, DynamoDbRepository>();
            services.AddSingleton<IDynamoDbRepository, DynamoDbRepository>();

            services.AddSingleton(new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 });

            services.AddSingleton<IDynamoDbFactoryModel<Payment, PaymentDbModel>, AutoMapperDynamoDbFactoryModel<Payment, PaymentDbModel>>();
            services.AddSingleton<IPaymentRepository, PaymentRepository>();

            services.AddSingleton<IPaymentProvider, ExternalPaymentProvider>();
            return services;
        }

        private static IAmazonDynamoDB GetAmazonDynamoDB(this IConfiguration configuration)
        {
            var accessKeyId = configuration.GetSection("AWS:AccessKey").Value;
            var secretAccessKey = configuration.GetSection("AWS:SecretKey").Value;
            var serviceUrl = configuration.GetSection("AWS:ServiceURL").Value;
            var clientConfig = new AmazonDynamoDBConfig
            {
                ServiceURL = serviceUrl,
                MaxErrorRetry = 10,
                ThrottleRetries = false
            };
            return new AmazonDynamoDBClient(accessKeyId, secretAccessKey, clientConfig);
        }
    }
}









// services.AddSingleton<IAmazonDynamoDB>(sp =>
// {
//     var credentials = new BasicAWSCredentials("root", "secret");
//     var config = new AmazonDynamoDBConfig { ServiceURL = "http://localhost:8000" };
//     return new AmazonDynamoDBClient(credentials, config);
// });