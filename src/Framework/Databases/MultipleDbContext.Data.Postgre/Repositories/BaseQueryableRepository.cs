using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MultipleContext.Data.Domain.Postgre;

namespace MultipleDbContext.Data.Postgre.Repositories;

public class BaseQueryableRepository<T, TKey> :IQueryableRepository<T, TKey> ,IQueryableRepositoryExtra<T, TKey>
    where T : class, IPostgreSqlEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    
    private readonly AppDbContext _readDbDatabaseContext;
    public BaseQueryableRepository(AppDbContext readDbDatabaseContext)
    {
        _readDbDatabaseContext =   readDbDatabaseContext ;
        
    }


    public async Task<List<T>> GetListAsync(Expression<Func<T, bool>>? predicate)
    {
        return predicate != null
            ? await _readDbDatabaseContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync()
            : await _readDbDatabaseContext.Set<T>().AsNoTracking().Where(c=> !c.IsDeleted).ToListAsync();  // default olarak isDeleted == false olarak ta işaretledik ancak burası değikenlik gösterebilir
    }

    public async  Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _readDbDatabaseContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async  Task<T?> GetByIdAsync(string id)
    {
        return await _readDbDatabaseContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(c=>c.Id.Equals(id));
    }

    public async  Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _readDbDatabaseContext.Set<T>().AsNoTracking().AnyAsync(predicate);
    }

    public async  Task<long> CountAsync(Expression<Func<T, bool>> predicate)
    {
        return await _readDbDatabaseContext.Set<T>().AsNoTracking().CountAsync(predicate);
    }

    public async  Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize)
    {
        return await _readDbDatabaseContext.Set<T>().AsNoTracking().Where(filter).Skip(pageIndex!=0? pageIndex*pageSize:pageSize).Take(pageSize).ToListAsync(CancellationToken.None);
    }
}