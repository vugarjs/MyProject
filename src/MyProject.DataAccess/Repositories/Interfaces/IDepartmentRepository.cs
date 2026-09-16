using MyProject.Entity.Models;

namespace MyProject.DataAccess.Repositories.Interfaces;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<Department?> GetByNameAsync(string name);
}
