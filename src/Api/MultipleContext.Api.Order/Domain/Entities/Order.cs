using MultipleContext.Data.Domain.Mongo;

namespace MultipleContext.Api.Order.Domain.Entities;

public class Order:IMongoEntity<string>
{
    public string Id { get; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
}