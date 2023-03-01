using Core.Models;

namespace Core.Repositories.OrderRepository;

public interface IOrderRepository
{
    public Task<IEnumerable<Order>> GetAllOrdersAsync(int count, int page);
    public Task<IEnumerable<Order>> GetAllUserOrdersAsync(Guid userId);
    public Task<Order> GetOrderAsync(Guid id);
    public Task<Order> CreateOrderAsync(Order order);
    public Task<Order> UpdateOrderAsync(Order order);
}