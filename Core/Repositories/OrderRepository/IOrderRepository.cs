using Core.Models;

namespace Core.Repositories.OrderRepository;

public interface IOrderRepository
{
    public Task<IQueryable<Order>> GetAllOrdersAsync();
    public Task<IQueryable<Order>> GetAllUserOrdersAsync(Guid userId);
    public Task<Order> GetOrderAsync(Guid id);
    public Task<Order> CreateOrderAsync(Order order);
    public Task<Order> UpdateOrderAsync(Order order);
}