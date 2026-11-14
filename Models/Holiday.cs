namespace EmployeeManagementSystem.Models;

public class Holiday
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string DayOfWeek => Date.ToString("dddd");
    public string Type { get; set; } = "Public Holiday";
    public string Description { get; set; } = string.Empty;
}

