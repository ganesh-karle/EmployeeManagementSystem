using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers;

[Authorize]
public class HolidayController : Controller
{
    private readonly ApplicationDbContext _context;

    public HolidayController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var holidays = await _context.Holidays
            .OrderBy(h => h.Date)
            .ToListAsync();

        return View(holidays);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Holiday holiday)
    {
        if (ModelState.IsValid)
        {
            _context.Holidays.Add(holiday);
            await _context.SaveChangesAsync();
            TempData["Success"] = "New holiday added to the calendar!";
        }
        return RedirectToAction(nameof(Index));
    }
}

