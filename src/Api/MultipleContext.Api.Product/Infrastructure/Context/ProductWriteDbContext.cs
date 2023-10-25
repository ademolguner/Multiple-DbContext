using Microsoft.EntityFrameworkCore;
using MultipleDbContext.Data.Sql;

namespace MultipleContext.Api.Product.Infrastructure.Context;

public class ProductWriteDbContext: AppDbContext
{
    public ProductWriteDbContext(DbContextOptions<ProductWriteDbContext> options) : base(options)
    {
    }
    
    public DbSet<Product.Domain.Entities.Product> Products { get; set; }
}