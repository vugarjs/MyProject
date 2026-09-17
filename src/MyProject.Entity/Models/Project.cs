using MyProject.Entity.Models.Common;

namespace MyProject.Entity.Models;

public class Project : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<EmployeeProject>? EmployeeProjects { get; set; }
}
