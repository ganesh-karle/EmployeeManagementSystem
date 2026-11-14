using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models;

public class Department
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Department name is required")]
    [StringLength(100)]
    [Display(Name = "Department Name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
