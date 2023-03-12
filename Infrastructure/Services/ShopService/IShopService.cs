using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.ShopService;

public interface IShopService
{
    public Task<IActionResult> GetOrderAsync(Guid id);
    public Task<IActionResult> GetOrdersAsync(int count, int page);
    public Task<IActionResult> CreateOrderAsync(Guid userId);
    public Task<IActionResult> UpdateOrderAsync(Guid orderId, Order.StatusType status);
}