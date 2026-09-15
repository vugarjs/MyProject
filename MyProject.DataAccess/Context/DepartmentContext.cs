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
            var stringConnection = "Data Source=localhost\\SQLEXPRESS;Database=CourseDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30"; // localhost => here your server name
            optionsBuilder.UseSqlServer(stringConnection);
        }
    }
}
