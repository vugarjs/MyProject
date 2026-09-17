using MyProject.Business.Services.Interfaces;
using MyProject.DataAccess.Repositories.Interfaces;
using MyProject.Entity.Models;

namespace MyProject.Business.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    public async Task<Employee> CreateAsync(Employee employee)
    {

        await _employeeRepository.AddAsync(employee); // Add the employee to the context
        await _employeeRepository.SaveChangesAsync();
        return employee;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _employeeRepository.FindAsync(e => e.Id == id);
        if (employee == null)
        {
            return false;
        }
        _employeeRepository.Remove(employee);
        await _employeeRepository.SaveChangesAsync();
        return true;
    }
    public async Task<Employee> UpdateAsync(Employee employee)
    {
        if (employee.Name == null || employee.Email == null)
        {
            throw new ArgumentException("Employee name and email cannot be null.");
        }
        _employeeRepository.Update(employee);
        await _employeeRepository.SaveChangesAsync();
        return employee;
    }
    public async Task<Employee> GetByIdAsync(int id)
    {
        var employee = await _employeeRepository.FindAsync(e => e.Id == id);
        return employee!;
    }
    public async Task<List<Employee>> GetAllAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees;
    }
}
