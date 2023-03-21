using System.Data.Entity;
using AutoMapper;
using Core.Models;
using Core.Repositories.CategoryRepository;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Extensions;
using Npgsql;

namespace Infrastructure.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IActionResult> CreateCategoryAsync(string name)
    {
        Category category = new Category(name);
        try
        {
            category = await _repository.CreateCategoryAsync(category);
        }
        catch (NpgsqlException e)
        {
            return new ObjectResult(e);
        }
        return new OkObjectResult(category);
    }

    public async Task<IActionResult> UpdateCategoryAsync(Guid id, string name)
    {
        Category category = await _repository.GetCategoryAsync(id);
        category.Name = name;
        try
        {
            await _repository.UpdateCategoryAsync(category);
        }
        catch (NpgsqlException e)
        {
            return new ObjectResult(e);
        }
        return new OkObjectResult(category);
    }

    public async Task<IActionResult> DeleteCategoryAsync(Guid id)
    {
        Category category = await _repository.GetCategoryAsync(id);
        try
        {
            await _repository.DeleteCategoryAsync(category);
        }
        catch (NpgsqlException e)
        {
            return new ObjectResult(e);
        }
        return new OkResult();
    }

    public async Task<IActionResult> GetAllCategoryAsync()
    {
        List<Category> categories = (await _repository.GetAllCategoriesAsync()).ToList();
        return new OkObjectResult(categories);
    }
    
    public async Task<IActionResult> GetAllCategoryAsync(int count, int page)
    {
        List<Category> categories = await (await _repository.GetAllCategoriesAsync())
            .Pagination(count, page).ToListAsync();
        if (categories.Count == 0) return new NotFoundResult();
        return new OkObjectResult(categories);
    }

    public async Task<IActionResult> GetCategoryAsync(Guid id)
    {
        try
        {
            Category category = await _repository.GetCategoryAsync(id);
            return new OkObjectResult(category);
        }
        catch (NpgsqlException e)
        {
            return new ObjectResult(e);
        }
    }
}