using Microsoft.EntityFrameworkCore;
using MyProject.Entity.Models;

namespace MyProject.DataAccess.Context
{
    public class DepartmentContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var stringConnection = "Server=localhost;Database=MyProjectDb;"; // localhost => here your server name
            optionsBuilder.UseSqlServer(stringConnection);
        }
    }
}
