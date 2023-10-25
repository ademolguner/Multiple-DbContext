using MultipleDbContext.Data;

namespace MultipleContext.Api.Product.Infrastructure.Repositories.Interfaces.Executable;

public interface IExecutableProductRepository:IExecutableRepository<Product.Domain.Entities.Product,int>
{
    
}