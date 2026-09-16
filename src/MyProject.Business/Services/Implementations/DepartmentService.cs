using Microsoft.EntityFrameworkCore;
using MyProject.Business.Services.Interfaces;
using MyProject.DataAccess.Context;
using MyProject.Entity.Models;

namespace MyProject.Business.Services.Implementations;

public class DepartmentService : IDepartmentService
{
    
    private readonly DepartmentContext _context;

    public DepartmentService(DepartmentContext context)
    {
        _context = context;
    }
    public async Task<Department> CreateAsync(Department department)
    {
        if(await _context.Departments.AnyAsync(d => d.Name == department.Name && d.Id != department.Id))
        {
            throw new Exception("Department with the same name already exists.");
        }

        await _context.Departments.AddAsync(department);
        await _context.SaveChangesAsync();

        return department;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null)
        {
            return false;
        }
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();

        return true;
    }
    public async Task<Department> UpdateAsync(Department department)
    {
    
        if(await _context.Departments.AnyAsync(d => d.Name == department.Name && d.Id != department.Id))
        {
            throw new Exception("Department with the same name already exists.");
        }
        _context.Departments.Update(department);
        await _context.SaveChangesAsync();
        return department;
    }
    public async Task<Department> GetByIdAsync(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        return department!;
    }
    public async Task<List<Department>> GetAllAsync()
    {
        var departments = await _context.Departments.ToListAsync();
        return departments;
    }
    public async Task<List<Department>> GetByNameAsync(string name)
    {
        var departments = await _context.Departments
            .Where(d => d.Name.Contains(name))
            .ToListAsync();

        return departments;
    }
}
