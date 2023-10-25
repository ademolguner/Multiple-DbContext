using Microsoft.EntityFrameworkCore;
using MultipleDbContext.Data.Postgre;

namespace MultipleContext.Api.Catalog.Infrastructure.Context;

public class CatalogWriteDbContext: AppDbContext
{
    public CatalogWriteDbContext(DbContextOptions<CatalogWriteDbContext> options) : base(options)
    {
    }
    
    public DbSet<Domain.Entities.Catalog> Catalogs { get; set; }
}