namespace EmployeeManagementSystem.Models.ViewModels;

public class DashboardViewModel
{
    public int TotalEmployees { get; set; }
    public int TotalDepartments { get; set; }
    public int PresentToday { get; set; }
    public int OnLeaveToday { get; set; }
    public int AvailableLeaves { get; set; } = 18;
    public decimal MonthlyPayroll { get; set; }
    public IEnumerable<Employee> RecentEmployees { get; set; } = new List<Employee>();
    public IEnumerable<Holiday> UpcomingHolidays { get; set; } = new List<Holiday>();
}
