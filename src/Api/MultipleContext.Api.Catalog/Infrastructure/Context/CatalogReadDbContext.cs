using Microsoft.EntityFrameworkCore;
using MultipleDbContext.Data.Postgre;

namespace MultipleContext.Api.Catalog.Infrastructure.Context;

public class CatalogReadDbContext: AppDbContext
{
     public CatalogReadDbContext(DbContextOptions<CatalogReadDbContext> options) : base(options)
     {
     }
    
    public DbSet<Domain.Entities.Catalog> Catalogs { get; set; }

    
}