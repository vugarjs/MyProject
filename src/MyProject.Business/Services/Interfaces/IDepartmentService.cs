using MyProject.Entity.Models;

namespace MyProject.Business.Services.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetAllAsync();
    Task<Department?> GetByIdAsync(int id);
    Task AddAsync(Department entity);
    void Update(Department entity);
    void Remove(int? id);
    Task<Department?> GetByNameAsync(string name);
}
