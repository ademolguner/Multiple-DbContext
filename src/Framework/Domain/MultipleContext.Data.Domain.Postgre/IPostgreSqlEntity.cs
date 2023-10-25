using MultipleDbContext.Data;

namespace MultipleContext.Data.Domain.Postgre;

public interface IPostgreSqlEntity<out TKey>:IEntity<TKey> where TKey : IEquatable<TKey>
{
    
}