 

using MultipleDbContext.Data;

namespace MultipleContext.Data.Domain.Sql;

public interface ISqlServerEntity<out TKey>:IEntity<TKey> where TKey : IEquatable<TKey>
{
    
}