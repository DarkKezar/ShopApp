using System.Net;
using Infrastructure.CustomResults;
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
    public async Task<ApiResult> GetCategoriesAsync()
    {
        return await _service.GetAllCategoryAsync();
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResult> CreateCategoryAsync(string name)
    {
        if (name == null) return new ApiResult("Input error", HttpStatusCode.BadRequest);
        return await _service.CreateCategoryAsync(name);
    }

    [HttpPatch]
    [Authorize(Roles = "Admin")]
    [Route("{id}")]
    public async Task<ApiResult> UpdateCategoryAsync(Guid id, string name)
    {
        if (id == default(Guid)) return new ApiResult("Input error", HttpStatusCode.BadRequest);
        return await _service.UpdateCategoryAsync(id, name);
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("{id}")]
    public async Task<ApiResult> DeleteCategoryAsync(Guid id)
    {
        if (id == default(Guid)) return new ApiResult("Input error", HttpStatusCode.BadRequest);
        return await _service.DeleteCategoryAsync(id);
    }
}