using Microsoft.EntityFrameworkCore;
using MyProject.DataAccess.Context;
using MyProject.DataAccess.Repositories.Interfaces;
using MyProject.Entity.Models.Common;
using System.Linq.Expressions;

namespace MyProject.DataAccess.Repositories.Implementations;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly DepartmentContext _context;
    protected readonly DbSet<T> _dbSet;
    public Repository(DepartmentContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        if (predicate != null)
        {
            query = _dbSet.Where(predicate);
        }
        if (orderBy != null)
        {
            query = orderBy(query);
        }
        if (includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return await query.ToListAsync();
    }

    public Task<T?> GetAsync(Expression<Func<T, bool>> predicate) => _dbSet.FirstOrDefaultAsync(predicate);

    public void Remove(T entity) => _dbSet.Remove(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public async Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null)
    {
        if (predicate != null)
        {
            return await _dbSet.AnyAsync(predicate);
        }
        return await _dbSet.AnyAsync();
    }
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public async Task<T?> FindAsync(Expression<Func<T, bool>>? predicate)
    {
        if (predicate != null)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
        return await _dbSet.FirstOrDefaultAsync();
    }
}
