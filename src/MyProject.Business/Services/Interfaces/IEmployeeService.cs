using MyProject.Entity.Models;

namespace MyProject.Business.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> CreateAsync(Employee employee);
        void Delete(int id);
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task<Employee> UpdateAsync(Employee employee);
    }
}