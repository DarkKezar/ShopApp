using Core.Models;
using Infrastructure.CustomResults;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.ShopService;

public interface IShopService
{
    public Task<ApiResult> GetOrderAsync(Guid id);
    public Task<ApiResult> GetOrdersAsync(int count, int page);
    public Task<ApiResult> CreateOrderAsync(Guid userId);
    public Task<ApiResult> UpdateOrderAsync(Guid orderId, Order.StatusType status);
}