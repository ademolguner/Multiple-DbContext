using MultipleContext.Data.Domain.Postgre;
using MultipleDbContext.Data;

namespace MultipleContext.Api.Catalog.Domain.Entities;

public class Catalog : IPostgreSqlEntity<int>
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public string Name { get; set; }
}