using System.Net;
using Core.Models;
using Infrastructure.CustomResults;
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
    public async Task<ApiResult> GetOrderAsync(Guid id)
    {
        if (id == default(Guid)) return new ApiResult("Input error", HttpStatusCode.BadRequest);
        return await _service.GetOrderAsync(id);
    }

    [HttpGet]
    public async Task<ApiResult> GetOrdersAsync(int count = 10, int page = 1)
    {
        return await _service.GetOrdersAsync(count, page);
    }

    [HttpPost]
    public async Task<ApiResult> CreateOrderAsync()
    {
        return await _service.CreateOrderAsync(this.GetCurrentUserId());
    }

    [HttpPatch]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResult> UpdateOrderAsync(Guid id, Order.StatusType status)
    {
        if (id == default(Guid) || status == default(Order.StatusType))
            return new ApiResult("Input error", HttpStatusCode.BadRequest);
        return await _service.UpdateOrderAsync(id, status);
    }
}