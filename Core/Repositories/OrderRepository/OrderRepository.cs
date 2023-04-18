using Core.Context;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.OrderRepository;

public class OrderRepository : IOrderRepository
{
    private readonly ShopContext _context;

    public OrderRepository(ShopContext context)
    {
        _context = context;
    }

    public async Task<IQueryable<Order>> GetAllOrdersAsync()
    {
        return _context.Orders;
    }

    public async Task<IQueryable<Order>> GetAllUserOrdersAsync(Guid userId)
    {
        return _context.Orders.Where(o => o.User.Id == userId);
    }

    public async Task<Order> GetOrderAsync(Guid id)
    {
        return await _context.Orders.SingleOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order> UpdateOrderAsync(Order order)
    {
        /*
         * This method can be used for delete operation
         */
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return order;
    }
}