using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
    public DbSet<Holiday> Holidays { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "IT", Description = "Information Technology" },
            new Department { Id = 2, Name = "HR", Description = "Human Resources" },
            new Department { Id = 3, Name = "Finance", Description = "Finance & Accounting" },
            new Department { Id = 4, Name = "Marketing", Description = "Marketing & Sales" }
        );
    }
}
