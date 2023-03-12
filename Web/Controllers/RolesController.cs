using Infrastructure.Services.AdminService;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class RolesController : Controller
{
    private readonly IAdminService _service;

    public RolesController(IAdminService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoleAsync(string name)
    {
        return await _service.CreateRoleAsync(name);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateRoleAsync(Guid id, string name)
    {
        return await _service.UpdateRoleAsync(id, name);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRoleAsync(Guid id)
    {
        return await _service.DeleteRoleAsync(id);
    }

    [HttpPost]
    [Route("Add-User")]
    public async Task<IActionResult> AddUserToRoleAsync(Guid userId, Guid roleId)
    {
        return await _service.AddUserToRoleAsync(userId, roleId);
    }
}