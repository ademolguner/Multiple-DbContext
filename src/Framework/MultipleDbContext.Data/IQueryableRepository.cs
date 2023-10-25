using System.Linq.Expressions;

namespace MultipleDbContext.Data;

public interface IQueryableRepository<T,TId> where T : class, IEntity<TId>,new() where TId : IEquatable<TId>
{
    Task<List<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetByIdAsync(string id);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<long> CountAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize);
}


public interface  IQueryableRepositoryExtra<T, TId> where T : class, IEntity<TId>, new() where TId : IEquatable<TId>
{
    // Farklı kosullar için ek imzalar olusturulabilir
    // Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);
}

public interface  IQueryableRepositoryMaster<T, TId> where T : class, IEntity<TId>, new() where TId : IEquatable<TId>
{
    Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate,bool master);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate,bool master);
    Task<T?> GetByIdAsync(string id,bool master);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate,bool master);
    Task<long> CountAsync(Expression<Func<T, bool>> predicate,bool master);
    Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize,bool master);
}