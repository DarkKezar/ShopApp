using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Web.Extensions;

public static class ControllerExtensions
{
    public static Guid GetUserId(this Controller controller)
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