using Microsoft.EntityFrameworkCore;

namespace MultipleDbContext.Data.Sql;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options): base(options)
    {
        
    }
}