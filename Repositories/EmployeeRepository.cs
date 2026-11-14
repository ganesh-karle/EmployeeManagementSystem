using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Employee>> GetAllAsync() =>
        await _context.Employees.Include(e => e.Department).ToListAsync();

    public async Task<Employee?> GetByIdAsync(int id) =>
        await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);

    public async Task AddAsync(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<(IEnumerable<Employee> Employees, int TotalCount)> GetFilteredAsync(
        string? search, int? departmentId, int page, int pageSize)
    {
        var query = _context.Employees.Include(e => e.Department).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim();
            query = query.Where(e => e.FirstName.Contains(s)
                || e.LastName.Contains(s)
                || e.Email.Contains(s)
                || e.PhoneNumber.Contains(s)
                || e.Designation.Contains(s)
                || (e.Department != null && e.Department.Name.Contains(s)));
        }

        if (departmentId.HasValue && departmentId > 0)
            query = query.Where(e => e.DepartmentId == departmentId);

        var totalCount = await query.CountAsync();
        var employees = await query.OrderBy(e => e.FirstName).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return (employees, totalCount);
    }
}
