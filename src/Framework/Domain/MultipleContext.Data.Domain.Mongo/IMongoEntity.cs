using MultipleDbContext.Data;

namespace MultipleContext.Data.Domain.Mongo;

public interface IMongoEntity<out TKey>:IEntity<TKey> where TKey : IEquatable<TKey>
{
    
}