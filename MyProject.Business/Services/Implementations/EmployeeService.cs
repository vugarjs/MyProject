using MyProject.DataAccess.Context;
using MyProject.Entity.Models;
using Microsoft.EntityFrameworkCore;
using MyProject.Business.Services.Interfaces;

namespace MyProject.Business.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    public async Task<Employee> CreateAsync(Employee employee)
    {
        var context = new DepartmentContext();
        if(employee.Name == null || employee.Email == null)
        {
            throw new ArgumentException("Employee name and email cannot be null.");
        }
        if(context.Departments.Any(x=> x.Limit <= context.Employees.Count(e => e.DepartmentId == x.Id) && x.Id == employee.DepartmentId))
        {
            throw new InvalidOperationException("Department limit exceeded.");
        }
        await context.Employees.AddAsync(employee); // Add the employee to the context
        await context.SaveChangesAsync();
        return employee;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var context = new DepartmentContext();
        var employee = await context.Employees.FindAsync(id);
        if (employee == null)
        {
            return false;
        }
        context.Employees.Remove(employee);
        await context.SaveChangesAsync();
        return true;
    }
    public async Task<Employee> UpdateAsync(Employee employee)
    {
        if(employee.Name == null || employee.Email == null)
        {
            throw new ArgumentException("Employee name and email cannot be null.");
        }
        var context = new DepartmentContext();
        context.Employees.Update(employee);
        await context.SaveChangesAsync();
        return employee;
    }
    public async Task<Employee> GetByIdAsync(int id)
    {
        var context = new DepartmentContext();
        var employee = await context.Employees
            .AsNoTracking<Employee>()
            .FirstOrDefaultAsync(e => e.Id == id);
        return employee!;
    }
    public async Task<List<Employee>> GetAllAsync()
    {
        var context = new DepartmentContext();
        var employees = await context.Employees
            .AsNoTracking<Employee>()
            .ToListAsync();
        return employees;
    }
}
