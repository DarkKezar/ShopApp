using System.Security.Claims;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Web.Extensions;

public static class ControllerExtensions
{
    
    public static JWTConfig GetJWTConfig(this Controller controller, IConfiguration _configuration)
    {
        return new JWTConfig(
            _configuration.GetValue<string>("Jwt:Issuer"),
            _configuration.GetValue<string>("Jwt:Audience"),
            _configuration.GetValue<string>("Jwt:Key"));
    }
    public static Guid GetCurrentUserId(this Controller controller)
    { 
        return Guid.Parse(controller.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Sid)
            .Select(c => c.Value).SingleOrDefault() ?? string.Empty);
    }

    public static List<string> GetCurrentUserRoles(this Controller controller)
    { 
        return controller.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value).ToList();
    }
}