using Infrastructure.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("category")]
public class CategoriesController : Controller
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("/categories")]
    public async Task<IActionResult> GetCategoriesAsync()
    {
        return await _service.GetAllCategoryAsync();
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCategoryAsync(string name)
    {
        if (name == null) return new BadRequestResult();
        return await _service.CreateCategoryAsync(name);
    }

    [HttpPatch]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCategoryAsync(Guid id, string name)
    {
        if (id == default(Guid)) return new BadRequestResult();
        return await _service.UpdateCategoryAsync(id, name);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCategoryAsync(Guid id)
    {
        if (id == default(Guid)) return new BadRequestResult();
        return await _service.DeleteCategoryAsync(id);
    }
}