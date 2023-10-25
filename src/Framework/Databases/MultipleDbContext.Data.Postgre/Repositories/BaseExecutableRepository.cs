using System.Linq.Expressions;
using MultipleContext.Data.Domain.Postgre;

namespace MultipleDbContext.Data.Postgre.Repositories;

public class BaseExecutableRepository<T,TKey>: IExecutableRepository<T,TKey> 
    where T : class,IPostgreSqlEntity<TKey>, new() 
    where TKey : IEquatable<TKey>
{
    
    private readonly AppDbContext _writeDbDatabaseContext;
    public BaseExecutableRepository(AppDbContext writeDbDatabaseContext)
    {
        _writeDbDatabaseContext =   writeDbDatabaseContext;
        
    }
    
    
    public async Task<bool> AddAsync(T entity)
    {
        await _writeDbDatabaseContext.Set<T>().AddAsync(entity);
        await _writeDbDatabaseContext.SaveChangesAsync(default);
        return true;
    }

    public async Task<bool> AddRangeAsync(List<T> entities)
    {
        await _writeDbDatabaseContext.Set<T>().AddRangeAsync(entities);
        await _writeDbDatabaseContext.SaveChangesAsync(default);
        return true;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _writeDbDatabaseContext.Set<T>().Update(entity);
        await _writeDbDatabaseContext.SaveChangesAsync(default);
        return true;
    }

    public  async Task<bool> DeleteAsync(string id)
    {
        var item = _writeDbDatabaseContext.Set<T>().FirstOrDefault(c => c.Id.Equals(id));
        if (item == null) 
            return false;
        
        _writeDbDatabaseContext.Set<T>().Remove(item);
        await _writeDbDatabaseContext.SaveChangesAsync(default);
        return true;
         
    }

    public async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate)
    {
        var item = _writeDbDatabaseContext.Set<T>().FirstOrDefault(predicate);
        if (item == null) 
            return false;
        
        _writeDbDatabaseContext.Set<T>().Remove(item);
        await _writeDbDatabaseContext.SaveChangesAsync(default);
        return true;
    }

    public async Task<bool> DeleteRangeAsync(Expression<Func<T, bool>> predicate)
    {
        var items = _writeDbDatabaseContext.Set<T>().Where(predicate).ToList();
        if (!items.Any()) 
            return false;
        
        _writeDbDatabaseContext.Set<T>().RemoveRange(items);
        await _writeDbDatabaseContext.SaveChangesAsync(default);
        return true;
    }
}