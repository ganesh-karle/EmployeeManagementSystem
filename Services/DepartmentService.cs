using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repositories;

namespace EmployeeManagementSystem.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;
    public DepartmentService(IDepartmentRepository repository) => _repository = repository;

    public Task<IEnumerable<Department>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Department?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task AddAsync(Department department) => _repository.AddAsync(department);
    public Task UpdateAsync(Department department) => _repository.UpdateAsync(department);
    public Task DeleteAsync(int id) => _repository.DeleteAsync(id);
}
