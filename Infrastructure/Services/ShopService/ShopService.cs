using System.Net;
using Core.Models;
using Core.Repositories.OrderRepository;
using Core.Repositories.ProductRepository;
using Core.Repositories.UserRepository;
using Infrastructure.CustomResults;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    
    public async Task<ApiResult> GetOrderAsync(Guid id)
    {
        try
        {
            Order order = await _repository.GetOrderAsync(id);
            return new ApiResult("Ok", HttpStatusCode.OK, order);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, (HttpStatusCode)500);
        }
    }

    public async Task<ApiResult> GetOrdersAsync(int count, int page)
    {
        try
        {
            List<Order> orders = await (await _repository.GetAllOrdersAsync()).
                Pagination(count, page).ToListAsync();
            return new ApiResult("Ok", HttpStatusCode.OK, orders);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, (HttpStatusCode)500);
        }
    }

    public async Task<ApiResult> CreateOrderAsync(Guid userId)
    {
        try
        {
            User user = await _userRepository.GetUserAsync(userId);
            Order order = new Order(user, user.ShoppingCart.Products);
            user.ShoppingCart.Products.Clear();
            user.Orders.Add(order);
            await _userRepository.UpdateUserAsync(user);
            return new ApiResult("Order has been created", HttpStatusCode.Created, order);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, (HttpStatusCode)500);
        }
    }

    public async Task<ApiResult> UpdateOrderAsync(Guid orderId, Order.StatusType status)
    {
        try
        {
            Order order = await _repository.GetOrderAsync(orderId);
            order.Status = status;
            order = await _repository.UpdateOrderAsync(order);

            return new ApiResult("Order has been updated", (HttpStatusCode)204, order);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, (HttpStatusCode)500);
        }
    }
    
}