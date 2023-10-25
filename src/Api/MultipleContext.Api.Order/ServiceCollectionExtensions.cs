using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using MultipleContext.Api.Order.Infrastructure.Context;
using MultipleContext.Api.Order.Infrastructure.Repositories.Concrete.Executable;
using MultipleContext.Api.Order.Infrastructure.Repositories.Concrete.Queryable;
using MultipleContext.Api.Order.Infrastructure.Repositories.Interfaces.Executable;
using MultipleContext.Api.Order.Infrastructure.Repositories.Interfaces.Queryable;
using MultipleDbContext.Data.Mongo;

namespace MultipleContext.Api.Order;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        return services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
    }

    
    public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services)
    {
        return services.AddScoped<IExecutableOrderRepository, ExecutableOrderRepository>()
                       .AddScoped<IQueryableOrderRepository, QueryableOrderRepository>();
         
    }
    
    public static IServiceCollection AddDbDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        services.AddScoped<IAppDbContext, OrderAppDbContext>();
        return services;
    }
    
    
    public static IServiceCollection AddClientDependencies(this IServiceCollection services)
    {
        return services.AddHttpClient();
    }
}