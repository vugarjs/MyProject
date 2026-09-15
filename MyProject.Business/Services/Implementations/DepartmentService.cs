using MyProject.DataAccess.Context;
using MyProject.Entity.Models;

namespace MyProject.Business.Services.Implementations;

public class DepartmentService
{
    
    // here crud methods 
    public async Task<Department> CreateAsync(Department department)
    {
        var context = new DepartmentContext();

        await context.Departments.AddAsync(department);
        await context.SaveChangesAsync();

        return department;
    }
}
