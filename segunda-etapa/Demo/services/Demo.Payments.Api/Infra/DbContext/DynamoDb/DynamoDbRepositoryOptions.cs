using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Extensions.NETCore.Setup;

namespace Demo.Payments.Api.Infra.DbContext.DynamoDb
{
    public class DynamoDbRepositoryOptions : AWSOptions
    {
        public string TableName { get; set; }
        public BillingMode BillingMode { get; set; }
        public ProvisionedThroughput ProvisionedThroughput { get; set; }
        public long ExpireDataAfterTime { get; set; } = 1;    }
}