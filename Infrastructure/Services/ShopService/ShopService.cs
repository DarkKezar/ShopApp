using Core.Models;
using Core.Repositories.OrderRepository;
using Core.Repositories.ProductRepository;
using Core.Repositories.UserRepository;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Infrastructure.Services.ShopService;

public class ShopService : IShopService
{
    private readonly IOrderRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;

    public ShopService(IOrderRepository repository, IUserRepository userRepository, 
        IProductRepository productRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _productRepository = productRepository;
    }
    
    public async Task<IActionResult> GetOrderAsync(Guid id)
    {
        try
        {
            return new OkObjectResult(await _repository.GetOrderAsync(id));
        }
        catch (Exception e)
        {
            return new NotFoundObjectResult(e.Message);
        }
    }

    public async Task<IActionResult> GetOrdersAsync(int count, int page)
    {
        try
        {
            return new OkObjectResult((await _repository.GetAllOrdersAsync()).Pagination(count, page).ToList());
        }
        catch (Exception e)
        {
            return new NotFoundObjectResult(e.Message);
        }
    }

    public async Task<IActionResult> CreateOrderAsync(Guid userId)
    {
        try
        {
            User user = await _userRepository.GetUserAsync(userId);
            Order order = new Order(user, user.ShoppingCart.Products);
            user.ShoppingCart.Products.Clear();
            user.Orders.Add(order);
            await _userRepository.UpdateUserAsync(user);
            return new OkObjectResult(order);
        }
        catch (Exception e)
        {
            return new ObjectResult(e.Message);
        }
    }

    public async Task<IActionResult> UpdateOrderAsync(Guid orderId, Order.StatusType status)
    {
        try
        {
            Order order = await _repository.GetOrderAsync(orderId);
            order.Status = status;
            order = await _repository.UpdateOrderAsync(order);

            return new OkObjectResult(order);
        }
        catch (Exception e)
        {
            return new ObjectResult(e.Message);
        }
    }
    
}