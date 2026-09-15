using MyProject.Entity.Models;

namespace MyProject.Business.Services.Interfaces;

public interface IDepartmentService
{
    Task<Department> CreateAsync(Department department);
    Task<bool> DeleteAsync(int id);
    Task<Department> UpdateAsync(Department department);
    Task<Department> GetByIdAsync(int id);
    Task<List<Department>> GetAllAsync();
    Task<List<Department>> GetByNameAsync(string name);

}
