using Core.Models;

namespace Core.Repositories.ProductRepository;

public interface IProductRepository
{
    public Task<IQueryable<Product>> GetAllProductsAsync();
    public Task<Product> GetProductAsync(Guid id);
    public Task<Product> CreatProductAsync(Product product);
    public Task<Product> UpdateProductAsync(Product product);
    public Task DeleteProductAsync(Product product);
    public Task DeleteProductAsync(Guid id);
}