using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MultipleContext.Api.Catalog.Infrastructure.Context;
using MultipleContext.Api.Catalog.Infrastructure.Repositories.Concrete.Executable;
using MultipleContext.Api.Catalog.Infrastructure.Repositories.Concrete.Queryable;
using MultipleContext.Api.Catalog.Infrastructure.Repositories.Interfaces.Executable;
using MultipleContext.Api.Catalog.Infrastructure.Repositories.Interfaces.Queryable;
using MultipleDbContext.Data.Postgre;

namespace MultipleContext.Api.Catalog;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        return services.Configure<PostgreDbSettings>(configuration.GetSection(nameof(PostgreDbSettings)));
    }

    
    public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services)
    {
        // framework e tası
        return services.AddScoped<IExecutableCatalogRepository, ExecutableCatalogRepository>()
                       .AddScoped<IQueryableCatalogRepository, QueryableCatalogRepository>();
         
    }
    public static IServiceCollection AddDbDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var postgreDbSettings = configuration.GetSection(nameof(PostgreDbSettings)).Get<PostgreDbSettings>() ?? throw new ArgumentNullException(nameof(PostgreDbSettings));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        services.AddDbContext<CatalogWriteDbContext>(options => options.UseNpgsql(postgreDbSettings.AppDbContextCommand));
        services.AddDbContext<CatalogReadDbContext>(options => options.UseNpgsql(postgreDbSettings.AppDbContextQuery));
        
        return services;
    }
    
    
    public static IServiceCollection AddClientDependencies(this IServiceCollection services)
    {
        return services.AddHttpClient();
    }
}