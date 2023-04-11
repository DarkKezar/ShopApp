using Infrastructure.CustomResults;
using Infrastructure.DTO.ProductTO;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.ProductService;

public interface IProductService
{
    public Task<ApiResult> CreateProductAsync(CreateProductTO model);
    public Task<ApiResult> UpdateProductAsync(UpdateProductTO model);
    public Task<ApiResult> DeleteProductAsync(DeleteProductTO model);
    public Task<ApiResult> GetAllProductsAsync(int count, int page);
    public Task<ApiResult> GetAllProductsAsync(List<Guid> categoriesId, int count, int page);
    public Task<ApiResult> GetProductAsync(Guid id);
}