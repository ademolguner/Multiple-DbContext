using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MultipleContext.Data.Domain.Postgre;

namespace MultipleDbContext.Data.Postgre.Repositories;

public class GenericBaseRepository<T,TKey>: IExecutableRepository<T,TKey> , IQueryableRepository<T, TKey> 
    where T : class,IPostgreSqlEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    
    private readonly AppDbContext _dbContext;
    public GenericBaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }



    #region Queryable Operation

    public async Task<List<T>> GetListAsync(Expression<Func<T, bool>>? predicate)
    {
        return predicate != null
            ? await _dbContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync()
            : await _dbContext.Set<T>().AsNoTracking().Where(c=> !c.IsDeleted).ToListAsync();  // default olarak isDeleted == false olarak ta işaretledik ancak burası değikenlik gösterebilir
    }

    public async  Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async  Task<T?> GetByIdAsync(string id)
    {
        return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(c=>c.Id.Equals(id));
    }

    public async  Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().AsNoTracking().AnyAsync(predicate);
    }

    public async  Task<long> CountAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().AsNoTracking().CountAsync(predicate);
    }

    public async  Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize)
    {
        return await _dbContext.Set<T>().AsNoTracking().Where(filter).Skip(pageIndex!=0? pageIndex*pageSize:pageSize).Take(pageSize).ToListAsync(CancellationToken.None);
    }

    #endregion


    #region Executable Operations

    public async Task<bool> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync(default);
        return true;
    }

    public async Task<bool> AddRangeAsync(List<T> entities)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync(default);
        return true;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync(default);
        return true;
    }

    public  async Task<bool> DeleteAsync(string id)
    {
        var item = _dbContext.Set<T>().FirstOrDefault(c => c.Id.Equals(id));
        if (item == null) 
            return false;
        
        _dbContext.Set<T>().Remove(item);
        await _dbContext.SaveChangesAsync(default);
        return true;
         
    }

    public async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate)
    {
        var item = _dbContext.Set<T>().FirstOrDefault(predicate);
        if (item == null) 
            return false;
        
        _dbContext.Set<T>().Remove(item);
        await _dbContext.SaveChangesAsync(default);
        return true;
    }

    public async Task<bool> DeleteRangeAsync(Expression<Func<T, bool>> predicate)
    {
        var items = _dbContext.Set<T>().Where(predicate).ToList();
        if (!items.Any()) 
            return false;
        
        _dbContext.Set<T>().RemoveRange(items);
        await _dbContext.SaveChangesAsync(default);
        return true;
    }

    #endregion
    
}