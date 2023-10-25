using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MultipleContext.Data.Domain.Mongo;

namespace MultipleDbContext.Data.Mongo.Repositories;

public class GenericBaseRepository<T,TKey>: IExecutableRepository<T,TKey> , IQueryableRepository<T, TKey> 
    where T : class,IMongoEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    protected readonly IAppDbContext _context;
    protected readonly IMongoCollection<T> _collection;
    protected GenericBaseRepository(IAppDbContext context)
    {
        _context = context;
        _collection = _context.GetCollection<T>();
    }

    
    #region Executable Operations

    
    public virtual async Task<bool> AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await _collection.InsertOneAsync(entity);
        return true;
    }

    public virtual async Task<bool> AddRangeAsync(List<T> entities)
    {
        entities.ForEach(entity =>
        {
            entity.CreatedAt = DateTime.UtcNow;
        });

        await _collection.InsertManyAsync(entities);
        return true;
    }

    public virtual async Task<bool> DeleteAsync(string id)
    {
        var update = Builders<T>.Update
            .Set(a => a.IsDeleted, true)
            .Set(a => a.UpdatedAt, DateTime.UtcNow);

        var result = await _collection.UpdateOneAsync(p => p.Id.Equals(id) && p.IsDeleted.Equals(false), update, new UpdateOptions { IsUpsert = false, BypassDocumentValidation = true });

        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate)
    {
        var update = Builders<T>.Update
            .Set(a => a.IsDeleted, true)
            .Set(a => a.UpdatedAt, DateTime.UtcNow);

        var result = await _collection.UpdateOneAsync(predicate, update, new UpdateOptions { IsUpsert = false, BypassDocumentValidation = true });

        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public virtual async Task<bool> DeleteRangeAsync(Expression<Func<T, bool>> predicate)
    {
        var update = Builders<T>.Update
            .Set(a => a.IsDeleted, true)
            .Set(a => a.UpdatedAt, DateTime.UtcNow);

        var result = await _collection.UpdateManyAsync(predicate, update, new UpdateOptions { IsUpsert = false, BypassDocumentValidation = true });

        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        var result = await _collection.ReplaceOneAsync(p => p.Id.Equals(entity.Id) && p.IsDeleted.Equals(false), entity);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
    
     #endregion


    #region Queryable Operations
     
    public virtual async Task<List<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null)
    { 
        var result = predicate == null
            ? await _collection.FindAsync(c => !c.IsDeleted) // default olarak isDeleted == false olarak ta işaretledik ancak burası değikenlik gösterebilir
            : await _collection.FindAsync(predicate);
        return await result.ToListAsync();
    }
    public virtual async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, bool master)
    {
        if (master)
            return await GetListAsync(predicate);

        var result = await _collection
            .WithReadPreference(ReadPreference.SecondaryPreferred)
            .FindAsync(predicate);
        return await result.ToListAsync();
    }

    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        var result = await _collection.FindAsync(predicate);
        return await result.FirstOrDefaultAsync();
    }
    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, bool master)
    {
        if (master)
            return await GetAsync(predicate);

        var result = await _collection
            .WithReadPreference(ReadPreference.SecondaryPreferred)
            .FindAsync(predicate);
        return await result.FirstOrDefaultAsync();
    }

    public virtual async Task<T?> GetByIdAsync(string id)
    {
        var result = await _collection.FindAsync(e => e.Id.Equals(id) && e.IsDeleted.Equals(false));
        return await result.FirstOrDefaultAsync();
    }
    public virtual async Task<T?> GetByIdAsync(string id, bool master)
    {
        if (master)
            return await GetByIdAsync(id);

        var result = await _collection
            .WithReadPreference(ReadPreference.SecondaryPreferred)
            .FindAsync(e => e.Id.Equals(id) && e.IsDeleted.Equals(false));
        return await result.FirstOrDefaultAsync();
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        var result = await _collection.CountDocumentsAsync(predicate);
        return result > 0;
    }
    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, bool master)
    {
        if (master)
            return await AnyAsync(predicate);

        var result = await _collection
            .WithReadPreference(ReadPreference.SecondaryPreferred)
            .CountDocumentsAsync(predicate);
        return result > 0;
    }


    public virtual async Task<long> CountAsync(Expression<Func<T, bool>> predicate)
    {
        var result = await _collection.CountDocumentsAsync(predicate);
        return result;
    }
    
    public virtual async Task<long> CountAsync(Expression<Func<T, bool>> predicate, bool master)
    {
        if (master)
            return await CountAsync(predicate);

        var result = await _collection
            .WithReadPreference(ReadPreference.SecondaryPreferred)
            .CountDocumentsAsync(predicate);
        return result;
    }

    public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize)
    {
        return await _collection.AsQueryable().Where(filter).OrderByDescending(x => x.CreatedAt)
            .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
    }
    
    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize, bool master)
    {
        if (master)
            return await FindAsync(filter, pageIndex, pageSize);

        return await _collection
            .WithReadPreference(ReadPreference.SecondaryPreferred)
            .AsQueryable()
            .Where(filter)
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
    #endregion
}