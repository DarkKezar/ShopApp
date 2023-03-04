using Infrastructure.DTO.ProductTO;
using Infrastructure.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : Controller
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int count = 10, int page = 0)
    {
        return await _service.GetAllProductsAsync(count, page);
    }

    [HttpGet]
    [Route("ByCategories")]
    public async Task<IActionResult> GetAllByCategoriesAsync(List<Guid> categoriesId, int count = 10, int page = 0)
    {
        return await _service.GetAllProductsAsync(categoriesId, count, page);
    }

    [HttpGet]
    [Route("Product")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        return await _service.GetProductAsync(id);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateProductTO model)
    {
        return await _service.CreateProductAsync(model);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateAsync(UpdateProductTO model)
    {
        return await _service.UpdateProductAsync(model);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(DeleteProductTO id)
    {
        return await _service.DeleteProductAsync(id);
    }
    
}