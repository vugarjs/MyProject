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

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
    {
        if (predicate == null)
        {
            return await _dbSet.ToListAsync();
        }

        return await _dbSet.Where(predicate).ToListAsync();
    }

    public Task<T?> GetAsync(Expression<Func<T, bool>> predicate) => _dbSet.FirstOrDefaultAsync(predicate);

    public void Remove(int id) => _dbSet.Remove(_dbSet.Find(id)!);

    public void Update(T entity) => _dbSet.Update(entity);
}
