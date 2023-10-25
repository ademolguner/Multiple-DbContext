using MultipleContext.Api.Order.Infrastructure.Context;
using MultipleContext.Api.Order.Infrastructure.Repositories.Interfaces.Queryable;
using MultipleDbContext.Data.Mongo;
using MultipleDbContext.Data.Mongo.Repositories;

namespace MultipleContext.Api.Order.Infrastructure.Repositories.Concrete.Queryable;

public class QueryableOrderRepository:BaseQueryableRepository<Domain.Entities.Order,string>,IQueryableOrderRepository
{
    public QueryableOrderRepository(IAppDbContext readDbDatabaseContext) : base(readDbDatabaseContext)
    {
    }
}