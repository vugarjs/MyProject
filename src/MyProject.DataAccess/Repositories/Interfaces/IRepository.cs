using MyProject.Entity.Models.Common;
using System.Linq.Expressions;

namespace MyProject.DataAccess.Repositories.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, // here we can filter the results (WHERE)
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, // here we can order the results (ORDER BY)
        params Expression<Func<T, object>>[] includes); // here we can include related entities (JOIN)
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null);
    Task SaveChangesAsync();
    Task<T?> FindAsync(Expression<Func<T, bool>>? predicate);
}