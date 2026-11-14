using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.ViewModels;

namespace EmployeeManagementSystem.Services;

public interface IEmployeeService
{
    Task<EmployeeListViewModel> GetEmployeesAsync(string? search, int? departmentId, int page, int pageSize);
    Task<Employee?> GetByIdAsync(int id);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(int id);
}
