using MultipleContext.Api.Order.Infrastructure.Repositories.Interfaces.Executable;
using MultipleDbContext.Data.Mongo;
using MultipleDbContext.Data.Mongo.Repositories;

namespace MultipleContext.Api.Order.Infrastructure.Repositories.Concrete.Executable;

public class ExecutableOrderRepository:BaseExecutableRepository<Domain.Entities.Order,string>,IExecutableOrderRepository
{
    public ExecutableOrderRepository(IAppDbContext writeDbDatabaseContext) : base(writeDbDatabaseContext)
    {
    }
}