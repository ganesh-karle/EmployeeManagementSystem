using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers;

[Authorize(Roles = "Admin")]
public class DepartmentController : Controller
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService) => _departmentService = departmentService;

    public async Task<IActionResult> Index()
    {
        var departments = await _departmentService.GetAllAsync();
        return View(departments);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Department department)
    {
        if (ModelState.IsValid)
        {
            await _departmentService.AddAsync(department);
            TempData["Success"] = "Department created successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(department);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var department = await _departmentService.GetByIdAsync(id);
        if (department == null) return NotFound();
        return View(department);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Department department)
    {
        if (id != department.Id) return NotFound();
        if (ModelState.IsValid)
        {
            await _departmentService.UpdateAsync(department);
            TempData["Success"] = "Department updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(department);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var department = await _departmentService.GetByIdAsync(id);
        if (department == null) return NotFound();
        return View(department);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _departmentService.DeleteAsync(id);
        TempData["Success"] = "Department deleted successfully!";
        return RedirectToAction(nameof(Index));
    }
}
