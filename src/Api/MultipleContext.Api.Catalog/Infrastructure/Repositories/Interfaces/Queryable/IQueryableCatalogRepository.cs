using MultipleDbContext.Data;

namespace MultipleContext.Api.Catalog.Infrastructure.Repositories.Interfaces.Queryable
{
    public interface IQueryableCatalogRepository:IQueryableRepository<Domain.Entities.Catalog,int>
    {
    
    }
}