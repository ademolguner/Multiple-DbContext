using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MultipleContext.Data.Domain.Mongo;

namespace MultipleDbContext.Data.Mongo.Repositories;

public class BaseQueryableRepository<T, TKey> :IQueryableRepository<T, TKey> ,
                                               IQueryableRepositoryMaster<T, TKey>
    where T : class, IMongoEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    protected readonly IAppDbContext Context;
    protected readonly IMongoCollection<T> Collection;

    protected BaseQueryableRepository(IAppDbContext context)
    {
        Context = context;
        Collection = Context.GetCollection<T>();
    }

    public virtual async Task<List<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null)
    { 
        var result = predicate == null
                                    ? await Collection.FindAsync(c => !c.IsDeleted)
                                    : await Collection.FindAsync(predicate);
        return await result.ToListAsync();
    }
    
    public virtual async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, bool master)
    {
        if (master)
            return await GetListAsync(predicate);

        var result = await Collection
            .WithReadPreference(ReadPreference.SecondaryPreferred)
            .FindAsync(predicate);
        return await result.ToListAsync();
    }

    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        var result = await Collection.FindAsync(predicate);
        return await result.FirstOrDefaultAsync();
    }
    
    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, bool master)
    {
        if (master)
            return await GetAsync(predicate);

        var result = await Collection
            .WithReadPreference(ReadPreference.SecondaryPreferred)
            .FindAsync(predicate);
        return await result.FirstOrDefaultAsync();
    }

    public virtual async Task<T?> GetByIdAsync(string id)
    {
        var result = await Collection.FindAsync(e => e.Id.Equals(id) && e.IsDeleted.Equals(false));
        return await result.FirstOrDefaultAsync();
    }
    public virtual async Task<T?> GetByIdAsync(string id, bool master)
    {
        if (master)
            return await GetByIdAsync(id);

        var result = await Collection
            .WithReadPreference(ReadPreference.SecondaryPreferred)
            .FindAsync(e => e.Id.Equals(id) && e.IsDeleted.Equals(false));
        return await result.FirstOrDefaultAsync();
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        var result = await Collection.CountDocumentsAsync(predicate);
        return result > 0;
    }
    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, bool master)
    {
        if (master)
            return await AnyAsync(predicate);

        var result = await Collection
            .WithReadPreference(ReadPreference.SecondaryPreferred)
            .CountDocumentsAsync(predicate);
        return result > 0;
    }


    public virtual async Task<long> CountAsync(Expression<Func<T, bool>> predicate)
    {
        var result = await Collection.CountDocumentsAsync(predicate);
        return result;
    }
    public virtual async Task<long> CountAsync(Expression<Func<T, bool>> predicate, bool master)
    {
        if (master)
            return await CountAsync(predicate);

        var result = await Collection
            .WithReadPreference(ReadPreference.SecondaryPreferred)
            .CountDocumentsAsync(predicate);
        return result;
    }

    public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize)
    {
        return await Collection.AsQueryable().Where(filter).OrderByDescending(x => x.CreatedAt)
            .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
    }
    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize, bool master)
    {
        if (master)
            return await FindAsync(filter, pageIndex, pageSize);

        return await Collection
            .WithReadPreference(ReadPreference.SecondaryPreferred)
            .AsQueryable()
            .Where(filter)
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}