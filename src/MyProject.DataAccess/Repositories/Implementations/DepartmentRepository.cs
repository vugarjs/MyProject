using MyProject.DataAccess.Context;
using MyProject.DataAccess.Repositories.Interfaces;
using MyProject.Entity.Models;

namespace MyProject.DataAccess.Repositories.Implementations;

public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    public DepartmentRepository(DepartmentContext context) : base(context)
    {

    }

    public async Task<Department?> GetByNameAsync(string name)
    {
        return await GetAsync(d => d.Name == name);
    }
}
