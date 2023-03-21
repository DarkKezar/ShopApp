using Infrastructure.DTO.ProductTO;
using Infrastructure.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [Route("/products")]
    public async Task<IActionResult> GetProductsAsync(int count = 10, int page = 1)
    {
        if (count <= 0 || page < 1) return new BadRequestResult();
        else return await _service.GetAllProductsAsync(count, page);
    }

    [HttpGet]
    [Route("/products/{categoriesId}")]
    public async Task<IActionResult> GetProductsByCategoriesAsync([FromQuery]List<Guid> categoriesId, 
        int count = 10, int page = 1)
    {
        if (count <= 0 || page < 1) return new BadRequestResult();
        else return await _service.GetAllProductsAsync(categoriesId, count, page);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetProductAsync(Guid id)
    {
        if (id == default(Guid)) return new BadRequestResult();
        return await _service.GetProductAsync(id);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProductAsync(CreateProductTO model)
    {
        if (model == null) return new BadRequestResult();
        return await _service.CreateProductAsync(model);
    }

    [HttpPatch]
    [Authorize(Roles = "Admin")]
    [Route("{id}")]
    public async Task<IActionResult> UpdateProductAsync(Guid Id, UpdateProductTO model)
    {
        if (Id == default(Guid)) return new BadRequestResult();
        model.Id = Id;
        return await _service.UpdateProductAsync(model);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("{id}")]
    public async Task<IActionResult> DeleteProductAsync(DeleteProductTO id)
    {
        if (id.Id == default(Guid)) return new BadRequestResult();
        return await _service.DeleteProductAsync(id);
    }
    
}