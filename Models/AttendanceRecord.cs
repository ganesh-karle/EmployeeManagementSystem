using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models;

public class AttendanceRecord
{
    public int Id { get; set; }

    [Required]
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.Today;

    public TimeSpan? ClockIn { get; set; }
    public TimeSpan? ClockOut { get; set; }

    [Required]
    [StringLength(50)]
    public string Status { get; set; } = "Present"; // Present, Late, Absent, On Leave, Half Day

    [StringLength(250)]
    public string? Remarks { get; set; }
}

