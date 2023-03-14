using Infrastructure.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : Controller
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync()
    {
        return await _service.GetAllCategoryAsync();
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCategoryAsync(string name)
    {
        return await _service.CreateCategoryAsync(name);
    }

    [HttpPatch]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCategoryAsync(Guid id, string name)
    {
        return await _service.UpdateCategoryAsync(id, name);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCategoryAsync(Guid id)
    {
        return await _service.DeleteCategoryAsync(id);
    }
}