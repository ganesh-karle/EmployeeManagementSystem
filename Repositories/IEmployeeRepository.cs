using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(int id);
    Task<(IEnumerable<Employee> Employees, int TotalCount)> GetFilteredAsync(string? search, int? departmentId, int page, int pageSize);
}
