using Microsoft.EntityFrameworkCore;
using MyProject.Entity.Models;
using MyProject.Entity.Models.Common;

namespace MyProject.DataAccess.Context
{
    public class DepartmentContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var stringConnection = "Data Source=localhost\\SQLEXPRESS;Database=CourseDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30"; // localhost => here your server name
            optionsBuilder.UseSqlServer(stringConnection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DepartmentContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditEntity>();
            foreach (var item in entries)
            {
               if(item.State == EntityState.Added)
                {
                    item.Entity.CreatedDate = DateTime.UtcNow;
                }
                else if (item.State == EntityState.Modified)
                {
                    item.Entity.UpdatedDate = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
