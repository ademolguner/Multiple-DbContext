using MultipleContext.Api.Catalog.Infrastructure.Context;
using MultipleContext.Api.Catalog.Infrastructure.Repositories.Interfaces.Executable;
using MultipleDbContext.Data.Postgre.Repositories;

namespace MultipleContext.Api.Catalog.Infrastructure.Repositories.Concrete.Executable;

public class ExecutableCatalogRepository:BaseExecutableRepository<Domain.Entities.Catalog,int>,IExecutableCatalogRepository
{
    public ExecutableCatalogRepository(CatalogWriteDbContext writeDbDatabaseContext) : base(writeDbDatabaseContext)
    {
    }
}