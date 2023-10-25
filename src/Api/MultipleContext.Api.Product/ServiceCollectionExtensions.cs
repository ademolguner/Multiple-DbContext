using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MultipleContext.Api.Product.Infrastructure.Context;
using MultipleContext.Api.Product.Infrastructure.Repositories.Concrete.Executable;
using MultipleContext.Api.Product.Infrastructure.Repositories.Concrete.Queryable;
using MultipleContext.Api.Product.Infrastructure.Repositories.Interfaces.Executable;
using MultipleContext.Api.Product.Infrastructure.Repositories.Interfaces.Queryable;
using MultipleDbContext.Data.Sql;

namespace MultipleContext.Api.Product;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        return services.Configure<SqlDbSettings>(configuration.GetSection(nameof(SqlDbSettings)));
    }

    
    public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services)
    {
        return services.AddScoped<IExecutableProductRepository, ExecutableProductRepository>()
                       .AddScoped<IQueryableProductRepository, QueryableProductRepository>();
         
    }
    public static IServiceCollection AddDbDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var dbSettings = configuration.GetSection(nameof(SqlDbSettings)).Get<SqlDbSettings>() ?? throw new ArgumentNullException(nameof(SqlDbSettings)); 
        services.AddDbContext<ProductWriteDbContext>(options => options.UseSqlServer(dbSettings.ExecutableDatabaseConnection));
        services.AddDbContext<ProductReadDbContext>(options => options.UseSqlServer(dbSettings.QueryableDatabaseConnection));
        return services;
    }
    
    
    public static IServiceCollection AddClientDependencies(this IServiceCollection services)
    {
        return services.AddHttpClient();
    }
}