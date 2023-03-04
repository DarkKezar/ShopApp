using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.AdminService;

public interface IAdminService
{
    public Task<IActionResult> CreateRoleAsync();
    public Task<IActionResult> UpdateRoleAsync();
    public Task<IActionResult> DeleteRoleAsync();
    public Task<IActionResult> UpdateOrderStatusAsync();
    public Task<IActionResult> AddUserToRoleAsync();
}