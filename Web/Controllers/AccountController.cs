using System.Text;
using Infrastructure.DTO;
using Infrastructure.DTO.AccountTO;
using Infrastructure.Services.AccountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly IAccountService _service;
    private readonly IConfiguration _configuration;

    private Guid GetIdOfCurrentUser()
    {
        return Guid.Parse(HttpContext.User.Claims.Where(c => c.Type == "Id")
            .Select(c => c.Value).SingleOrDefault() ?? string.Empty);
    }
    
    public AccountController(IAccountService service, IConfiguration configuration)
    {
        _service = service;
        _configuration = configuration;
    }
    

    [HttpPost]
    [Route("Sign-Up")]
    public async Task<IActionResult> RegisterUserAsync(CreateAccountTO model)
    {
        return await _service.RegisterUserAsync(model);
    }
    
    [HttpPost]
    [Route("Sign-In")]
    [AllowAnonymous]
    public async Task<IActionResult> AuthorizeUserAsync(AuthorizeAccountTO model)
    {
        JWTConfig config = new JWTConfig(
            _configuration.GetValue<string>("Jwt:Issuer"),
            _configuration.GetValue<string>("Jwt:Audience"),
            Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
        return await _service.AuthorizeUserAsync(model, config);
    }

    [HttpPatch]
    [Route("Add-To-Cart")]
    public async Task<IActionResult> AddToCartAsync(List<Guid> productsId)
    {
        return await _service.AddToCartAsync(GetIdOfCurrentUser(), productsId);
    }
}