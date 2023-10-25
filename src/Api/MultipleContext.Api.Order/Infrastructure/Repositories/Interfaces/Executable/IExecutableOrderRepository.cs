using MultipleDbContext.Data;

namespace MultipleContext.Api.Order.Infrastructure.Repositories.Interfaces.Executable;

public interface IExecutableOrderRepository:IExecutableRepository<Order.Domain.Entities.Order,string>
{
    
}