using Microsoft.EntityFrameworkCore;
using MultipleDbContext.Data.Sql;

namespace MultipleContext.Api.Product.Infrastructure.Context;

public class ProductReadDbContext: AppDbContext
{
     public ProductReadDbContext(DbContextOptions<ProductReadDbContext> options) : base(options)
     {
     }
    
    public DbSet<Domain.Entities.Product> Products { get; set; }

    
}