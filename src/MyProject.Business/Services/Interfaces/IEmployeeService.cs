using MyProject.Entity.Models;

namespace MyProject.Business.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> CreateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
        Task<Employee> UpdateAsync(Employee employee);
        Task<Employee> GetByIdAsync(int id);
        Task<List<Employee>> GetAllAsync();
    }
}