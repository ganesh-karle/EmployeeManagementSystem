using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Data;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();

        // Ensure tables exist if database was pre-created
        await context.Database.ExecuteSqlRawAsync(@"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Holidays')
            BEGIN
                CREATE TABLE [Holidays] (
                    [Id] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    [Name] nvarchar(max) NOT NULL,
                    [Date] datetime2 NOT NULL,
                    [Type] nvarchar(max) NOT NULL,
                    [Description] nvarchar(max) NOT NULL
                );
            END;

            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AttendanceRecords')
            BEGIN
                CREATE TABLE [AttendanceRecords] (
                    [Id] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    [EmployeeId] int NOT NULL,
                    [Date] datetime2 NOT NULL,
                    [ClockIn] time NULL,
                    [ClockOut] time NULL,
                    [Status] nvarchar(50) NOT NULL,
                    [Remarks] nvarchar(250) NULL,
                    CONSTRAINT [FK_AttendanceRecords_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE CASCADE
                );
            END;
        ");

        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = { "Admin", "Employee" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        var adminEmail = "admin@ems.com";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var admin = new ApplicationUser { UserName = adminEmail, Email = adminEmail, FullName = "System Admin", EmailConfirmed = true };
            await userManager.CreateAsync(admin, "Admin@123");
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        var empEmail = "employee@ems.com";
        if (await userManager.FindByEmailAsync(empEmail) == null)
        {
            var emp = new ApplicationUser { UserName = empEmail, Email = empEmail, FullName = "John Employee", EmailConfirmed = true };
            await userManager.CreateAsync(emp, "Emp@1234");
            await userManager.AddToRoleAsync(emp, "Employee");
        }

        // Seed initial employees if none exist
        if (!context.Employees.Any())
        {
            context.Employees.AddRange(
                new Employee
                {
                    FirstName = "Ganesh",
                    LastName = "Karle",
                    Email = "ganeshkarle1234@gmail.com",
                    PhoneNumber = "+91 7972848203",
                    DateOfBirth = new DateTime(2000, 5, 15),
                    HireDate = new DateTime(2023, 7, 1),
                    Designation = "Senior .NET Developer",
                    Salary = 85000,
                    DepartmentId = 1
                },
                new Employee
                {
                    FirstName = "Priya",
                    LastName = "Sharma",
                    Email = "priya.sharma@ems.com",
                    PhoneNumber = "+91 9823456789",
                    DateOfBirth = new DateTime(1998, 8, 22),
                    HireDate = new DateTime(2022, 3, 15),
                    Designation = "HR Manager",
                    Salary = 72000,
                    DepartmentId = 2
                },
                new Employee
                {
                    FirstName = "Rahul",
                    LastName = "Verma",
                    Email = "rahul.verma@ems.com",
                    PhoneNumber = "+91 9123456780",
                    DateOfBirth = new DateTime(1995, 11, 10),
                    HireDate = new DateTime(2021, 1, 10),
                    Designation = "Lead Financial Analyst",
                    Salary = 90000,
                    DepartmentId = 3
                },
                new Employee
                {
                    FirstName = "Sneha",
                    LastName = "Patil",
                    Email = "sneha.patil@ems.com",
                    PhoneNumber = "+91 9876501234",
                    DateOfBirth = new DateTime(1999, 3, 5),
                    HireDate = new DateTime(2023, 9, 1),
                    Designation = "Marketing Strategist",
                    Salary = 65000,
                    DepartmentId = 4
                },
                new Employee
                {
                    FirstName = "Amit",
                    LastName = "Deshmukh",
                    Email = "amit.deshmukh@ems.com",
                    PhoneNumber = "+91 9988776655",
                    DateOfBirth = new DateTime(1996, 12, 18),
                    HireDate = new DateTime(2022, 6, 1),
                    Designation = "Backend Engineer",
                    Salary = 78000,
                    DepartmentId = 1
                },
                new Employee
                {
                    FirstName = "Ananya",
                    LastName = "Kulkarni",
                    Email = "ananya.k@ems.com",
                    PhoneNumber = "+91 9112233445",
                    DateOfBirth = new DateTime(1997, 7, 25),
                    HireDate = new DateTime(2022, 11, 15),
                    Designation = "Talent Acquisition Specialist",
                    Salary = 68000,
                    DepartmentId = 2
                }
            );
            await context.SaveChangesAsync();
        }

        // Seed Holidays
        if (!context.Holidays.Any())
        {
            context.Holidays.AddRange(
                new Holiday { Name = "New Year's Day", Date = new DateTime(2027, 1, 1), Type = "Public Holiday", Description = "Global celebration of the New Year" },
                new Holiday { Name = "Republic Day", Date = new DateTime(2027, 1, 26), Type = "National Holiday", Description = "Celebration of the Constitution of India" },
                new Holiday { Name = "Maha Shivratri", Date = new DateTime(2027, 3, 6), Type = "Festive Holiday", Description = "Annual festival honoring Lord Shiva" },
                new Holiday { Name = "Holi (Festival of Colors)", Date = new DateTime(2027, 3, 23), Type = "National Festival", Description = "Celebration of spring and colors" },
                new Holiday { Name = "Eid al-Fitr", Date = new DateTime(2027, 4, 9), Type = "Religious Holiday", Description = "End of Ramadan celebration" },
                new Holiday { Name = "Maharashtra Day / Labour Day", Date = new DateTime(2027, 5, 1), Type = "State & Labour Holiday", Description = "Formation of Maharashtra State" },
                new Holiday { Name = "Independence Day", Date = new DateTime(2027, 8, 15), Type = "National Holiday", Description = "Indian Independence Day commemoration" },
                new Holiday { Name = "Ganesh Chaturthi", Date = new DateTime(2027, 9, 4), Type = "Regional & Cultural", Description = "Festival celebrating Lord Ganesha" },
                new Holiday { Name = "Gandhi Jayanti", Date = new DateTime(2027, 10, 2), Type = "National Holiday", Description = "Mahatma Gandhi birthday celebration" },
                new Holiday { Name = "Diwali (Deepavali)", Date = new DateTime(2027, 10, 29), Type = "Major National Festival", Description = "Festival of lights and prosperity" },
                new Holiday { Name = "Christmas Day", Date = new DateTime(2026, 12, 25), Type = "Public Holiday", Description = "Celebration of Christmas" }
            );
            await context.SaveChangesAsync();
        }

        // Seed Attendance for employees
        if (!context.AttendanceRecords.Any() && context.Employees.Any())
        {
            var empList = context.Employees.ToList();
            var today = DateTime.Today;
            foreach (var emp in empList)
            {
                context.AttendanceRecords.Add(new AttendanceRecord
                {
                    EmployeeId = emp.Id,
                    Date = today,
                    ClockIn = new TimeSpan(9, 15, 0),
                    ClockOut = new TimeSpan(18, 30, 0),
                    Status = emp.Id % 5 == 0 ? "On Leave" : (emp.Id % 4 == 0 ? "Late" : "Present"),
                    Remarks = emp.Id % 5 == 0 ? "Casual Leave Approved" : (emp.Id % 4 == 0 ? "Late Check-In at 09:45 AM" : "Standard On-Time Check-In")
                });
            }
            await context.SaveChangesAsync();
        }
    }
}
