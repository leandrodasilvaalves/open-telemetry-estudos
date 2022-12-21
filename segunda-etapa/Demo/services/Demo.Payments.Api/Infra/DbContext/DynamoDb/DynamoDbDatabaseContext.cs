using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Demo.Payments.Api.Infra.DbContext.DynamoDb.Tables;

namespace Demo.Payments.Api.Infra.DbContext.DynamoDb
{
    public class DynamoDbDatabaseContext : IDatabaseContext
    {

        private readonly IAmazonDynamoDB _dynamoDb;
        private readonly DynamoDbRepositoryOptions _options;

        public DynamoDbDatabaseContext(IAmazonDynamoDB dynamoDb, DynamoDbRepositoryOptions options)
        {
            _dynamoDb = dynamoDb ?? throw new ArgumentNullException(nameof(dynamoDb));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task ConfigureAsync()
        {
            var request = new CustomTableRequest(_options);
            await CreateIfNotExist(request, _options.TableName);
        }

        public async Task<bool> TableExist(string tableName)
        {
            var tables = await _dynamoDb.ListTablesAsync();
            var existTable = tables.TableNames.Contains(tableName);
            return existTable;
        }

        private async Task CreateIfNotExist(CreateTableRequest request, string tableName)
        {
            if (await TableExist(tableName)) { return; }
            await _dynamoDb.CreateTableAsync(request);
        }
    }
}