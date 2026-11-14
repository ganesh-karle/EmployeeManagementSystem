namespace EmployeeManagementSystem.Models.ViewModels;

public class UserRoleInfo
{
    public string UserId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class RoleAccessViewModel
{
    public List<UserRoleInfo> Users { get; set; } = new();
    public List<string> Roles { get; set; } = new();
}

