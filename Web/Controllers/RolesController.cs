using Infrastructure.Services.RoleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("role")]
[Authorize(Roles = "Admin")]
public class RolesController : Controller
{
    private readonly IRoleService _service;

    public RolesController(IRoleService service)
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
        if (id == default(Guid)) return new BadRequestResult();
        return await _service.UpdateRoleAsync(id, name);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRoleAsync(Guid id)
    {
        if (id == default(Guid)) return new BadRequestResult();
        return await _service.DeleteRoleAsync(id);
    }
    
}