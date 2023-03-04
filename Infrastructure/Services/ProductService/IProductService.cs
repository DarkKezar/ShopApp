using Infrastructure.DTO.ProductTO;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.ProductService;

public interface IProductService
{
    public Task<IActionResult> CreateProductAsync(CreateProductTO model);
    public Task<IActionResult> UpdateProductAsync(UpdateProductTO model);
    public Task<IActionResult> DeleteProductAsync(DeleteProductTO model);
    public Task<IActionResult> GetAllProductsAsync(int count, int page);
    public Task<IActionResult> GetAllProductsAsync(List<Guid> categoriesId, int count, int page);
    public Task<IActionResult> GetProductAsync(Guid id);
}