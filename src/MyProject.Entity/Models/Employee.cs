using MyProject.Entity.Models.Common;

namespace MyProject.Entity.Models;

public class Employee : AuditEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int DepartmentId { get; set; } // Foreign key to the Department entity
    public Department Department { get; set; } = null!;

    public List<EmployeeProject>? EmployeeProjects { get; set; }
}
