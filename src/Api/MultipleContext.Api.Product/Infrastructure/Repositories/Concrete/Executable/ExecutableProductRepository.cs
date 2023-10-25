using MultipleContext.Api.Product.Infrastructure.Context;
using MultipleContext.Api.Product.Infrastructure.Repositories.Interfaces.Executable;
using MultipleDbContext.Data.Sql.Repositories;

namespace MultipleContext.Api.Product.Infrastructure.Repositories.Concrete.Executable;

public class ExecutableProductRepository:BaseExecutableRepository<Product.Domain.Entities.Product,int>,IExecutableProductRepository
{
    public ExecutableProductRepository(ProductWriteDbContext writeDbDatabaseContext) : base(writeDbDatabaseContext)
    {
    }
}