using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace Demo.Payments.Api.Infra.DbContext.DynamoDb.Tables
{
    public class CustomTableRequest : CreateTableRequest
    {
        public CustomTableRequest(DynamoDbRepositoryOptions options)
        {
            TableName = options.TableName;
            AttributeDefinitions = new List<AttributeDefinition>
            {
                new AttributeDefinition("PartitionKey", ScalarAttributeType.S),
                new AttributeDefinition("SortKey", ScalarAttributeType.S),
            };
            KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement("PartitionKey", KeyType.HASH),
                new KeySchemaElement("SortKey", KeyType.RANGE)
            };
            BillingMode = options.BillingMode;
            ProvisionedThroughput = options.ProvisionedThroughput;
        }
    }
}