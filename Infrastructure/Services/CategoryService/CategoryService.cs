using AutoMapper;
using Core.Models;
using Core.Repositories.CategoryRepository;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Extensions;

namespace Infrastructure.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly CategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(CategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<IActionResult> CreateCategoryAsync(string name)
    {
        Category category = new Category(name);
        category = await _repository.CreateCategoryAsync(category);
        return new ObjectResult(category);
    }

    public async Task<IActionResult> UpdateCategoryAsync(Guid id, string name)
    {
        Category category = await _repository.GetCategoryAsync(id);
        category.Name = name;
        await _repository.UpdateCategoryAsync(category);
        return new OkResult();
    }

    public async Task<IActionResult> DeleteCategoryAsync(Guid id)
    {
        Category category = await _repository.GetCategoryAsync(id);
        await _repository.DeleteCategoryAsync(category);
        return new OkResult();
    }

    public async Task<IActionResult> GetAllCategoryAsync(int count, int page)
    {
        IEnumerable<Category> categories = (await _repository.GetAllCategoriesAsync()).Pagination(count, page);
        return new ObjectResult(categories.ToList());
    }

    public async Task<IActionResult> GetCategoryAsync(Guid id)
    {
        return new ObjectResult(await _repository.GetCategoryAsync(id));
    }
}