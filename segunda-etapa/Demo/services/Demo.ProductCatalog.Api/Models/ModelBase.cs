using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Demo.ProductCatalog.Api.Models
{
    public abstract class ModelBase
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

    }
}