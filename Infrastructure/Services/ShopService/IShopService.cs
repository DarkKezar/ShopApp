using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.ShopService;

public interface IShopService
{
    public Task<IActionResult> AddToShoppingCartAsync();
    public Task<IActionResult> RemoveFromShoppingCartAsync();
    public Task<IActionResult> ClearShoppingCart();
    public Task<IActionResult> CreateOrderAsync();
    public Task<IActionResult> DeleteOrderAsync();
}