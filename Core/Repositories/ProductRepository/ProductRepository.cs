using Core.Context;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.ProductRepository;

public class ProductRepository : IProductRepository
{
    private readonly ShopContext _context;

    public ProductRepository(ShopContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(int count, int page)
    {
        return _context.Products.Skip(count * page).Take(count);
    }

    public async Task<Product> GeProductAsync(Guid id)
    {
        return await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> CreatProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProductAsync(Product product)
    {
        _context.ProductStats.Remove(product.ProductStats);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}