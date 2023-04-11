using System.Data.Entity;
using AutoMapper;
using Core.Models;
using Core.Repositories.CategoryRepository;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Extensions;
using Npgsql;
using Infrastructure.CustomResults;
using System.Net;

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

    public async Task<ApiResult> CreateCategoryAsync(string name)
    {
        Category category = new Category(name);
        try
        {
            category = await _repository.CreateCategoryAsync(category);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, (HttpStatusCode)500);;
        }
        return new ApiResult("Category has been created", HttpStatusCode.Created, category);
    }

    public async Task<ApiResult> UpdateCategoryAsync(Guid id, string name)
    {
        Category category = await _repository.GetCategoryAsync(id);
        category.Name = name;
        try
        {
            await _repository.UpdateCategoryAsync(category);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, (HttpStatusCode)500);;
        }
        return new ApiResult("Category has been updated", (HttpStatusCode)204, category);
    }

    public async Task<ApiResult> DeleteCategoryAsync(Guid id)
    {
        Category category = await _repository.GetCategoryAsync(id);
        try
        {
            await _repository.DeleteCategoryAsync(category);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, (HttpStatusCode)500);;
        }
        return new ApiResult("Category has been deleted", (HttpStatusCode)204, id);
    }

    public async Task<ApiResult> GetAllCategoryAsync()
    {
        List<Category> categories = (await _repository.GetAllCategoriesAsync()).ToList();
        return new ApiResult("Ok", HttpStatusCode.OK, categories);
;
    }
    
    public async Task<ApiResult> GetAllCategoryAsync(int count, int page)
    {
        List<Category> categories = await (await _repository.GetAllCategoriesAsync())
            .Pagination(count, page).ToListAsync();
        if (categories.Count == 0) return new ApiResult("Categories not found", HttpStatusCode.NotFound);
        return new ApiResult("Ok", HttpStatusCode.OK, categories);
    }

    public async Task<ApiResult> GetCategoryAsync(Guid id)
    {
        try
        {
            Category category = await _repository.GetCategoryAsync(id);
            return new ApiResult("Ok", HttpStatusCode.OK, category);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, (HttpStatusCode)500);;
        }
    }
}