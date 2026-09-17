using Microsoft.EntityFrameworkCore;
using MyProject.DataAccess.Configuration;
using MyProject.Entity.Models;
using MyProject.Entity.Models.Common;

namespace MyProject.DataAccess.Context
{
    public class DepartmentContext : DbContext
    {
        // 1. DI (Program.cs) tərəfindən tətbiq olunması üçün mütləq olan konstruktor
        public DepartmentContext(DbContextOptions<DepartmentContext> options) : base(options)
        {
        }

        // 2. Parametrsiz konstruktor (Əgər EF Core tools tərəfindən birbaşa çağırılarsa lazımdır)
        public DepartmentContext()
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // DI tərəfindən konfiqurasiya olunmayıbsa, bu hardcoded string-i işlət
            if (!optionsBuilder.IsConfigured)
            {
                var stringConnection = "Data Source=localhost\\SQLEXPRESS;Database=CourseDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";
                optionsBuilder.UseSqlServer(stringConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfigration());
            modelBuilder.ApplyConfiguration(new EmployeeConfigration());
            modelBuilder.ApplyConfiguration(new ProjectConfigration());
            modelBuilder.ApplyConfiguration(new EmployeeProjectConfigration());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditEntity>();
            foreach (var item in entries)
            {
                if (item.State == EntityState.Added)
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