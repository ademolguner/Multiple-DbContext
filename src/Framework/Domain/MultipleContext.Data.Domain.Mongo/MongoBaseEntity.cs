using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultipleContext.Data.Domain.Mongo;

public class MongoBaseEntity:IMongoEntity<string>
{
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonId]
    [BsonElement(Order = 0)]
    public required string Id { get; set; }

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement(Order = 101)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [BsonRepresentation(BsonType.DateTime)]
    [BsonElement(Order = 101)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime? UpdatedAt { get; set; }

    [BsonElement(Order = 105)]
    public bool IsDeleted { get; set; }
}