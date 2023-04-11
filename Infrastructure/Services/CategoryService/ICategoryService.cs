using Infrastructure.CustomResults;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.CategoryService;

public interface ICategoryService
{
    public Task<ApiResult> CreateCategoryAsync(string name);
    public Task<ApiResult> UpdateCategoryAsync(Guid id, string name);
    public Task<ApiResult> DeleteCategoryAsync(Guid id);
    public Task<ApiResult> GetAllCategoryAsync();
    public Task<ApiResult> GetAllCategoryAsync(int count, int page);
    public Task<ApiResult> GetCategoryAsync(Guid id);
}