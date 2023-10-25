using MultipleContext.Api.Product.Infrastructure.Context;
using MultipleContext.Api.Product.Infrastructure.Repositories.Interfaces.Queryable;
using MultipleDbContext.Data.Sql.Repositories;

namespace MultipleContext.Api.Product.Infrastructure.Repositories.Concrete.Queryable;

public class QueryableProductRepository:BaseQueryableRepository<Product.Domain.Entities.Product,int>,IQueryableProductRepository
{
    public QueryableProductRepository(ProductReadDbContext readDbDatabaseContext) : base(readDbDatabaseContext)
    {
    }
}