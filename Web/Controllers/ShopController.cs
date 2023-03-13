using Core.Models;
using Infrastructure.Services.ShopService;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ShopController : Controller
{
    private readonly IShopService _service;

    public ShopController(IShopService service)
    {
        _service = service;
    }
    
    private Guid GetIdOfCurrentUser() //наверное надо куда-то вынести
    {
        return Guid.Parse(HttpContext.User.Claims.Where(c => c.Type == "Id")
            .Select(c => c.Value).SingleOrDefault() ?? string.Empty);
    }

    [HttpGet]
    [Route("Order/{id}")]
    public async Task<IActionResult> GetOrderAsync(Guid id)
    {
        return await _service.GetOrderAsync(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync(int count, int page)
    {
        return await _service.GetOrdersAsync(count, page);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync()
    {
        return await _service.CreateOrderAsync(GetIdOfCurrentUser());
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateOrderAsync(Guid id, Order.StatusType status)
    {
        return await _service.UpdateOrderAsync(id, status);
    }
}