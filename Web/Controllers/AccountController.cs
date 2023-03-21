using System.Text;
using Core.Context;
using Core.Models;
using Infrastructure.DTO;
using Infrastructure.DTO.AccountTO;
using Infrastructure.Services.AccountService;
using Infrastructure.Services.RoleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Extensions;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly IAccountService _service;
    private readonly IRoleService _adminService;
    private readonly IConfiguration _configuration;

    public AccountController(IAccountService service, IRoleService adminService, IConfiguration configuration)
    {
        _service = service;
        _adminService = adminService;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("Sign-Up")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUserAsync(CreateAccountTO model)
    {
        if (model == null) return new BadRequestResult();
        else return await _service.RegisterUserAsync(model);
    }
    
    [HttpPost]
    [Route("Sign-In")]
    [AllowAnonymous]
    public async Task<IActionResult> AuthorizeUserAsync(AuthorizeAccountTO model)
    {
        if (model == null) return new BadRequestResult();
        return await _service.AuthorizeUserAsync(model, this.GetJWTConfig(_configuration));
    }

    [HttpPatch]
    [Route("Add-To-Cart")]
    [Authorize]
    public async Task<IActionResult> AddToCartAsync(List<Guid> productsId)
    {
        if (productsId == null || productsId.Count == 0) return new BadRequestResult();
        return await _service.AddToCartAsync(this.GetCurrentUserId(), productsId);
    }
    
    [HttpPost]
    [Route("add-role")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> AddUserToRoleAsync(Guid userId, Guid roleId)
    {
        if (userId == default(Guid)) return new BadRequestResult();
        return await _adminService.AddUserToRoleAsync(userId, roleId);
    }
}