using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Demo.ProductCatalog.Api.Models
{
    public abstract class ModelBase
    {
        public ModelBase()
        {
            Id ??= ObjectId.GenerateNewId().ToString();
        }

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

    }
}