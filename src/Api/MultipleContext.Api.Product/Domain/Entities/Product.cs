using MultipleContext.Data.Domain.Sql;

namespace MultipleContext.Api.Product.Domain.Entities;

public class Product : ISqlServerEntity<int>
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public string Name { get; set; }
}