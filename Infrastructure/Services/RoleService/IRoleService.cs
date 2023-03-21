using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.RoleService;

public interface IRoleService
{
    public Task<IActionResult> CreateRoleAsync(string name);
    public Task<IActionResult> UpdateRoleAsync(Guid id, string name);
    public Task<IActionResult> DeleteRoleAsync(Guid id);
    public Task<IActionResult> AddUserToRoleAsync(Guid userId, Guid roleId);
}