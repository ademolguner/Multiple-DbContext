using System.Linq.Expressions;
using MongoDB.Driver;
using MultipleContext.Data.Domain.Mongo;

namespace MultipleDbContext.Data.Mongo.Repositories;

public class BaseExecutableRepository<T,TKey>: IExecutableRepository<T,TKey> 
    where T : class,IMongoEntity<TKey>, new() 
    where TKey : IEquatable<TKey>
{
    protected readonly IAppDbContext Context;
    protected readonly IMongoCollection<T> Collection;
    protected BaseExecutableRepository(IAppDbContext context)
    {
        Context = context;
        Collection = Context.GetCollection<T>();
    }

    
    public virtual async Task<bool> AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await Collection.InsertOneAsync(entity);
        return true;
    }

    public virtual async Task<bool> AddRangeAsync(List<T> entities)
    {
        entities.ForEach(entity =>
        {
            entity.CreatedAt = DateTime.UtcNow;
        });

        await Collection.InsertManyAsync(entities);
        return true;
    }

    public virtual async Task<bool> DeleteAsync(string id)
    {
        var update = Builders<T>.Update
            .Set(a => a.IsDeleted, true)
            .Set(a => a.UpdatedAt, DateTime.UtcNow);

        var result = await Collection.UpdateOneAsync(p => p.Id.Equals(id) && p.IsDeleted.Equals(false), update, new UpdateOptions { IsUpsert = false, BypassDocumentValidation = true });

        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate)
    {
        var update = Builders<T>.Update
            .Set(a => a.IsDeleted, true)
            .Set(a => a.UpdatedAt, DateTime.UtcNow);

        var result = await Collection.UpdateOneAsync(predicate, update, new UpdateOptions { IsUpsert = false, BypassDocumentValidation = true });

        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public virtual async Task<bool> DeleteRangeAsync(Expression<Func<T, bool>> predicate)
    {
        var update = Builders<T>.Update
            .Set(a => a.IsDeleted, true)
            .Set(a => a.UpdatedAt, DateTime.UtcNow);

        var result = await Collection.UpdateManyAsync(predicate, update, new UpdateOptions { IsUpsert = false, BypassDocumentValidation = true });

        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        var result = await Collection.ReplaceOneAsync(p => p.Id.Equals(entity.Id) && p.IsDeleted.Equals(false), entity);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
}