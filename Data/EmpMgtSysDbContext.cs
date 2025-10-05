using EmployeesManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagementSystem.Data
{
    public class EmpMgtSysDbContext: DbContext
    {
        public EmpMgtSysDbContext(DbContextOptions<EmpMgtSysDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> Employee_Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the composite primary key for EmployeeProject
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId });
        }
    }
}
