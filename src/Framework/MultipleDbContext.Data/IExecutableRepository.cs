using System.Linq.Expressions;

namespace MultipleDbContext.Data;

public interface IExecutableRepository<T,TId> where T : class, IEntity<TId>,new() where TId : IEquatable<TId>
{
    Task<bool> AddAsync(T entity);
    Task<bool> AddRangeAsync(List<T> entities);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(string id);
    Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate);
    Task<bool> DeleteRangeAsync(Expression<Func<T, bool>> predicate);
}