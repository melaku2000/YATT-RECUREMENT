using System.Linq.Expressions;

namespace YattCommon
{
    public interface IRepository<T> where T:IEntity
    {
        Task<T> CreateAsync(T entity);
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
