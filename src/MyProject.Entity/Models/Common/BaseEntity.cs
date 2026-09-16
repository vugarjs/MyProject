namespace MyProject.Entity.Models.Common;

public class BaseEntity
{
    public int Id { get; set; }
}
public class AuditEntity : BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
