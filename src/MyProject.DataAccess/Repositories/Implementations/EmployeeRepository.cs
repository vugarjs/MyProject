using MyProject.DataAccess.Context;
using MyProject.DataAccess.Repositories.Interfaces;
using MyProject.Entity.Models;

namespace MyProject.DataAccess.Repositories.Implementations;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(DepartmentContext context) : base(context)
    {

    }
}
