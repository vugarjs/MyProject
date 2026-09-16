using MyProject.DataAccess.Context;
using MyProject.Entity.Models;
using Microsoft.EntityFrameworkCore;
using MyProject.Business.Services.Interfaces;

namespace MyProject.Business.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly DepartmentContext _context;
    public EmployeeService(DepartmentContext context)
    {
        _context = context;
    }
    public async Task<Employee> CreateAsync(Employee employee)
    {
        if (employee.Name == null || employee.Email == null)
        {
            throw new ArgumentException("Employee name and email cannot be null.");
        }
        if(await _context.Departments.AnyAsync(x=> x.Limit <= _context.Employees.Count(e => e.DepartmentId == x.Id) && x.Id == employee.DepartmentId))
        {
            throw new InvalidOperationException("Department limit exceeded.");
        }
        await _context.Employees.AddAsync(employee); // Add the employee to the context
        await _context.SaveChangesAsync();
        return employee;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return false;
        }
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<Employee> UpdateAsync(Employee employee)
    {
        if(employee.Name == null || employee.Email == null)
        {
            throw new ArgumentException("Employee name and email cannot be null.");
        }
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        return employee;
    }
    public async Task<Employee> GetByIdAsync(int id)
    {
        var employee = await _context.Employees
            .AsNoTracking<Employee>()
            .FirstOrDefaultAsync(e => e.Id == id);
        return employee!;
    }
    public async Task<List<Employee>> GetAllAsync()
    {
        var employees = await _context.Employees
            .AsNoTracking<Employee>()
            .ToListAsync();
        return employees;
    }
}
