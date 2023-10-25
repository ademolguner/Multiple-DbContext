using MultipleDbContext.Data;

namespace MultipleContext.Api.Order.Infrastructure.Repositories.Interfaces.Queryable
{
    public interface IQueryableOrderRepository:IQueryableRepository<Order.Domain.Entities.Order,string>,IQueryableRepositoryMaster<Order.Domain.Entities.Order,string>
    {
    
    }
}