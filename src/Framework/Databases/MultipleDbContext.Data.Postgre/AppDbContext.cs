using Microsoft.EntityFrameworkCore;

namespace MultipleDbContext.Data.Postgre;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options): base(options)
    {
        
    }
}