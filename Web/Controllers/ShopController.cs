using Core.Models;
using Infrastructure.Services.ShopService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;

namespace Web.Controllers;

[ApiController]
[Route("orders")]
[Authorize]
public class ShopController : Controller
{
    private readonly IShopService _service;

    public ShopController(IShopService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Route("order/{id}")]
    public async Task<IActionResult> GetOrderAsync(Guid id)
    {
        if (id == default(Guid)) return new BadRequestResult();
        return await _service.GetOrderAsync(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync(int count = 10, int page = 1)
    {
        return await _service.GetOrdersAsync(count, page);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync()
    {
        return await _service.CreateOrderAsync(this.GetCurrentUserId());
    }

    [HttpPatch]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateOrderAsync(Guid id, Order.StatusType status)
    {
        if (id == default(Guid) || status == null) return new BadRequestResult();
        return await _service.UpdateOrderAsync(id, status);
    }
}