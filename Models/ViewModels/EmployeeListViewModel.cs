namespace EmployeeManagementSystem.Models.ViewModels;

public class EmployeeListViewModel
{
    public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
    public string? SearchTerm { get; set; }
    public int? DepartmentFilter { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
    public int PageSize { get; set; } = 5;
    public IEnumerable<Department> Departments { get; set; } = new List<Department>();
}
