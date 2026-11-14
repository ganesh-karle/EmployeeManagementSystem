using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers;

[Authorize]
public class AttendanceController : Controller
{
    private readonly ApplicationDbContext _context;

    public AttendanceController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(DateTime? date, string? status)
    {
        var targetDate = date ?? DateTime.Today;
        ViewBag.SelectedDate = targetDate;
        ViewBag.SelectedStatus = status;

        var query = _context.AttendanceRecords
            .Include(a => a.Employee)
            .ThenInclude(e => e!.Department)
            .Where(a => a.Date.Date == targetDate.Date);

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(a => a.Status == status);
        }

        var records = await query.ToListAsync();

        // If today has no records generated yet, ensure employees have records
        if (!records.Any() && targetDate.Date == DateTime.Today)
        {
            var employees = await _context.Employees.ToListAsync();
            foreach (var emp in employees)
            {
                var record = new AttendanceRecord
                {
                    EmployeeId = emp.Id,
                    Date = DateTime.Today,
                    ClockIn = new TimeSpan(9, 15, 0),
                    ClockOut = new TimeSpan(18, 30, 0),
                    Status = emp.Id % 5 == 0 ? "On Leave" : (emp.Id % 4 == 0 ? "Late" : "Present"),
                    Remarks = emp.Id % 5 == 0 ? "Casual Leave Approved" : (emp.Id % 4 == 0 ? "Late check-in at 09:45 AM" : "Standard On-Time Check-In")
                };
                _context.AttendanceRecords.Add(record);
            }
            await _context.SaveChangesAsync();

            records = await _context.AttendanceRecords
                .Include(a => a.Employee)
                .ThenInclude(e => e!.Department)
                .Where(a => a.Date.Date == targetDate.Date)
                .ToListAsync();
        }

        ViewBag.TotalPresent = records.Count(r => r.Status == "Present");
        ViewBag.TotalLate = records.Count(r => r.Status == "Late");
        ViewBag.TotalLeave = records.Count(r => r.Status == "On Leave");
        ViewBag.TotalAbsent = records.Count(r => r.Status == "Absent");

        return View(records);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Mark(int employeeId, string status, string? remarks)
    {
        var record = await _context.AttendanceRecords
            .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.Date.Date == DateTime.Today);

        if (record != null)
        {
            record.Status = status;
            record.Remarks = remarks ?? $"Updated to {status}";
            _context.AttendanceRecords.Update(record);
        }
        else
        {
            _context.AttendanceRecords.Add(new AttendanceRecord
            {
                EmployeeId = employeeId,
                Date = DateTime.Today,
                ClockIn = new TimeSpan(9, 0, 0),
                Status = status,
                Remarks = remarks ?? $"Marked as {status}"
            });
        }

        await _context.SaveChangesAsync();
        TempData["Success"] = "Attendance record updated successfully!";
        return RedirectToAction(nameof(Index));
    }
}

