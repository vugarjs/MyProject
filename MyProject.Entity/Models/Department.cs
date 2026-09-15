using MyProject.Entity.Models.Common;

namespace MyProject.Entity.Models;

public class Department : AuditEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Limit { get; set; }
    public List<Employee> Employees { get; set; } = new List<Employee>();
}
