namespace EmployeeManagementSystem.Models.ViewModels;

public class EmployeeMonthlyAttendanceViewModel
{
    public Employee Employee { get; set; } = null!;
    public int Month { get; set; }
    public int Year { get; set; }
    public string MonthName => new DateTime(Year, Month, 1).ToString("MMMM yyyy");
    public int TotalWorkDays { get; set; }
    public int DaysPresent { get; set; }
    public int DaysLate { get; set; }
    public int DaysOnLeave { get; set; }
    public int AvailableLeaveBalance { get; set; } = 18;
    public List<AttendanceRecord> Records { get; set; } = new();
}

