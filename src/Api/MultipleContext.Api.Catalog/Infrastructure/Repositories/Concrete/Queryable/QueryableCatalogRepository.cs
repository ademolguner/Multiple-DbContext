using MultipleContext.Api.Catalog.Infrastructure.Context;
using MultipleContext.Api.Catalog.Infrastructure.Repositories.Interfaces.Queryable;
using MultipleDbContext.Data.Postgre.Repositories;

namespace MultipleContext.Api.Catalog.Infrastructure.Repositories.Concrete.Queryable;

public class QueryableCatalogRepository:BaseQueryableRepository<Domain.Entities.Catalog,int>,IQueryableCatalogRepository
{
    public QueryableCatalogRepository(CatalogReadDbContext readDbDatabaseContext) : base(readDbDatabaseContext)
    {
    }
}