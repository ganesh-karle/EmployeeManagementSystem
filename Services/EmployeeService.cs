using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Models.ViewModels;
using EmployeeManagementSystem.Repositories;

namespace EmployeeManagementSystem.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly IDepartmentRepository _departmentRepository;

    public EmployeeService(IEmployeeRepository repository, IDepartmentRepository departmentRepository)
    {
        _repository = repository;
        _departmentRepository = departmentRepository;
    }

    public async Task<EmployeeListViewModel> GetEmployeesAsync(string? search, int? departmentId, int page, int pageSize)
    {
        var (employees, totalCount) = await _repository.GetFilteredAsync(search, departmentId, page, pageSize);
        var departments = await _departmentRepository.GetAllAsync();

        return new EmployeeListViewModel
        {
            Employees = employees,
            SearchTerm = search,
            DepartmentFilter = departmentId,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            PageSize = pageSize,
            Departments = departments
        };
    }

    public Task<Employee?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task AddAsync(Employee employee) => _repository.AddAsync(employee);
    public Task UpdateAsync(Employee employee) => _repository.UpdateAsync(employee);
    public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
}
