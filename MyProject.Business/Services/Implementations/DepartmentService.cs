using Microsoft.EntityFrameworkCore;
using MyProject.Business.Services.Interfaces;
using MyProject.DataAccess.Context;
using MyProject.Entity.Models;

namespace MyProject.Business.Services.Implementations;

public class DepartmentService : IDepartmentService
{
    
    // here crud methods 
    public async Task<Department> CreateAsync(Department department)
    {
        var context = new DepartmentContext();
        if(context.Departments.Any(d => d.Name == department.Name && d.Id != department.Id))
        {
            throw new Exception("Department with the same name already exists.");
        }

        await context.Departments.AddAsync(department);
        await context.SaveChangesAsync();

        return department;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var context = new DepartmentContext();
        var department = await context.Departments.FindAsync(id);
        if (department == null)
        {
            return false;
        }
        context.Departments.Remove(department);
        await context.SaveChangesAsync();
        return true;
    }
    public async Task<Department> UpdateAsync(Department department)
    {
        var context = new DepartmentContext();
        if(context.Departments.Any(d => d.Name == department.Name && d.Id != department.Id))
        {
            throw new Exception("Department with the same name already exists.");
        }
        context.Departments.Update(department);
        await context.SaveChangesAsync();
        return department;
    }
    public async Task<Department> GetByIdAsync(int id)
    {
        var context = new DepartmentContext();
        var department = await context.Departments.FindAsync(id);
        return department;
    }
    public async Task<List<Department>> GetAllAsync()
    {
        var context = new DepartmentContext();
        var departments = await context.Departments.ToListAsync();
        return departments;
    }
    public async Task<List<Department>> GetByNameAsync(string name)
    {
        var context = new DepartmentContext();
        var departments = await context.Departments
            .Where(d => d.Name.Contains(name))
            .ToListAsync();

        return departments;
    }
}
