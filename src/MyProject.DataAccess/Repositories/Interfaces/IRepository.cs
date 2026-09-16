using MyProject.Entity.Models.Common;
using System.Linq.Expressions;

namespace MyProject.DataAccess.Repositories.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(int id);
}