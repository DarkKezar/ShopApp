using Infrastructure.CustomResults;
using Infrastructure.DTO.ProductTO;
using Infrastructure.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Web.Controllers;

[ApiController]
[Route("product")]
public class ProductsController : Controller
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("/products/{page}")]
    [ResponseCache(CacheProfileName = "Default30")]
    public async Task<ApiResult> GetProductsAsync(int count = 10, int page = 1)
    {
        if (count <= 0 || page < 1) return new ApiResult("Input error", HttpStatusCode.BadRequest);
        else return await _service.GetAllProductsAsync(count, page);
    }

    [HttpGet]
    [Route("/products")]
    public async Task<ApiResult> GetProductsByCategoriesAsync([FromQuery]List<Guid> categoriesId, 
        int count = 10, int page = 1)
    {
        if (count <= 0 || page < 1) return new ApiResult("Input error", HttpStatusCode.BadRequest);
        else return await _service.GetAllProductsAsync(categoriesId, count, page);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ApiResult> GetProductAsync(Guid id)
    {
        if (id == default(Guid)) return new ApiResult("Input error", HttpStatusCode.BadRequest);
        return await _service.GetProductAsync(id);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResult> CreateProductAsync(CreateProductTO model)
    {
        if (model == null) return new ApiResult("Input error", HttpStatusCode.BadRequest);
        return await _service.CreateProductAsync(model);
    }

    [HttpPatch]
    [Authorize(Roles = "Admin")]
    [Route("{id}")]
    public async Task<ApiResult> UpdateProductAsync(Guid Id, UpdateProductTO model)
    {
        if (Id == default(Guid)) return new ApiResult("Input error", HttpStatusCode.BadRequest);
        model.Id = Id;
        return await _service.UpdateProductAsync(model);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("{id}")]
    public async Task<ApiResult> DeleteProductAsync(DeleteProductTO id)
    {
        if (id.Id == default(Guid)) return new ApiResult("Input error", HttpStatusCode.BadRequest);
        return await _service.DeleteProductAsync(id);
    }
    
}