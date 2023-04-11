using System.Net;
using Infrastructure.CustomResults;
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
    public async Task<ApiResult> CreateRoleAsync(string name)
    {
        return await _service.CreateRoleAsync(name);
    }

    [HttpPatch]
    [Route("{id}")]
    public async Task<ApiResult> UpdateRoleAsync(Guid id, string name)
    {
        if (id == default(Guid)) return new ApiResult("Input error", HttpStatusCode.BadRequest);
        return await _service.UpdateRoleAsync(id, name);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ApiResult> DeleteRoleAsync(Guid id)
    {
        if (id == default(Guid)) return new ApiResult("Input error", HttpStatusCode.BadRequest);
        return await _service.DeleteRoleAsync(id);
    }
    
}