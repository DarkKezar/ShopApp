using Core.Context;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.CategoryRepository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ShopContext _context;

    public CategoryRepository(ShopContext context)
    {
        _context = context;
    }

    public async Task<IQueryable<Category>> GetAllCategoriesAsync()
    {
        return _context.Categories;
    }

    public async Task<Category> GetCategoryAsync(Guid id)
    {
        return await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}