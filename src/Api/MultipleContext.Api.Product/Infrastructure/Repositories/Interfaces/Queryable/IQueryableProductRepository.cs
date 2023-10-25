using MultipleDbContext.Data;

namespace MultipleContext.Api.Product.Infrastructure.Repositories.Interfaces.Queryable
{
    public interface IQueryableProductRepository:IQueryableRepository<Product.Domain.Entities.Product,int>
    {
    
    }
}