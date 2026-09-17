using MyProject.Entity.Models;

namespace MyProject.Business.Services.Interfaces;

public interface IDepartmentService
{
    Task AddAsync(Department entity);
    Task DeleteAsync(int? id);
    Task<IEnumerable<Department>> GetAllAsync();

    Task<Department> UpdateAsync(Department department);
    Task<Department?> GetByIdAsync(int id);
    Task<Department?> GetByNameAsync(string name);

}
