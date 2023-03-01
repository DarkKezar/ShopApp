using Core.Models;

namespace Core.Repositories.CategoryRepository;

public interface ICategoryRepository
{
    public Task<IEnumerable<Category>> GetAllCategoriesAsync(int count, int page);
    public Task<Category> GetCategoryAsync(Guid id);
    public Task<Category> CreateCategoryAsync(Category category);
    public Task<Category> UpdateCategoryAsync(Category category);
    public Task DeleteCategoryAsync(Category category);
}