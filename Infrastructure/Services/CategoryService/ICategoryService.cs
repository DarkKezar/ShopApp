using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.CategoryService;

public interface ICategoryService
{
    public Task<IActionResult> CreateCategoryAsync(string name);
    public Task<IActionResult> UpdateCategoryAsync(Guid id, string name);
    public Task<IActionResult> DeleteCategoryAsync(Guid id);
    public Task<IActionResult> GetAllCategoryAsync(int count, int page);
    public Task<IActionResult> GetCategoryAsync(Guid id);
}