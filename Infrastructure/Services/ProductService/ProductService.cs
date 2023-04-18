using AutoMapper;
using Core.Models;
using Core.Repositories.CategoryRepository;
using Core.Repositories.ProductRepository;
using FluentValidation.Results;
using Infrastructure.AutoMappers;
using Infrastructure.DTO.ProductTO;
using Infrastructure.Validators.ProductValidators;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Net;
using Infrastructure.CustomResults;

namespace Infrastructure.Services.ProductService;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    
    public ProductService(IProductRepository repository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ApiResult> CreateProductAsync(CreateProductTO model)
    {
        ValidationResult result = await (new CreateProductTOValidator()).ValidateAsync(model);
        if (result.IsValid)
        {
            List<Category> categories = (await _categoryRepository.GetAllCategoriesAsync())
                .Where(c => model.CategoriesId.Contains(c.Id)).ToList();
            Product product = new Product(
                model.Name,
                categories,
                model.Price,
                new ProductStats(model.PhotoUrl, model.SomeData)
                );
            try
            {
                product = await _repository.CreatProductAsync(product);
            }
            catch (Exception e)
            {
                return new ApiResult(e.Message, HttpStatusCode.InternalServerError);
            }
            return new ApiResult("Product has been created!", HttpStatusCode.Created, product);
        }
        else
        {
            return new ApiResult("Model has errors", HttpStatusCode.BadRequest, result.Errors);
        }
    }

    public async Task<ApiResult> UpdateProductAsync(UpdateProductTO model)
    {
        ValidationResult result = await (new UpdateProductTOValidator()).ValidateAsync(model);
        if (result.IsValid)
        {
            
            try
            {
                Product product = await _repository.GetProductAsync(model.Id);
                product.Name = (!model.NewData.Name.IsNullOrEmpty()) ?
                    model.NewData.Name : product.Name;
                product.ProductStats.PhotoUrl = (!model.NewData.PhotoUrl.IsNullOrEmpty()) ?
                    model.NewData.PhotoUrl : product.ProductStats.PhotoUrl;
                product.ProductStats.SomeData = (!model.NewData.SomeData.IsNullOrEmpty()) ? 
                    model.NewData.SomeData : product.ProductStats.SomeData;
                
                product = await _repository.UpdateProductAsync(product);
                return new ApiResult("Product(s) have(has) been updated.", (HttpStatusCode)204 ,product);
            }
            catch (Exception e)
            {
                return new ApiResult(e.Message, (HttpStatusCode)500);
            }
        }
        else
        {
            return new ApiResult("Model has errors", HttpStatusCode.BadRequest, result.Errors);
        }
    }

    public async Task<ApiResult> DeleteProductAsync(DeleteProductTO model)
    {
        ValidationResult result = await (new DeleteProductToValidator()).ValidateAsync(model);
        if (result.IsValid)
        {
            try
            {
                await _repository.DeleteProductAsync(model.Id);
            }
            catch (Exception e)
            {
                return new ApiResult(e.Message, (HttpStatusCode)500);
            }
            return new ApiResult("Product has been deleted.", (HttpStatusCode)204, model.Id);
        }
        else
        {
            return new ApiResult("Model has errors", HttpStatusCode.BadRequest, result.Errors);
        }
    }

    public async Task<ApiResult> GetAllProductsAsync(int count, int page)
    {
        try
        {
            List<Product> products = (await _repository.GetAllProductsAsync())
                .Include(p => p.Categories)
                .Pagination(count, page).ToList();
            return new ApiResult("Ok", HttpStatusCode.OK, products);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, (HttpStatusCode)500);
        }
    }
    public async Task<ApiResult> GetAllProductsAsync(List<Guid> categoriesId, int count, int page)
    {
        try
        {
            List<Category> categories = await (await _categoryRepository.GetAllCategoriesAsync())
                .Where(c => categoriesId.Contains(c.Id))
                .Include(c => c.Products)
                .ThenInclude(p => p.ProductStats)
                .ToListAsync();
            if (categories.Count == 0) return new ApiResult("Categories not found", HttpStatusCode.NotFound);

            List<Product> products = new List<Product>();
            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    if (!products.Contains(product))
                    {
                        products.Add(product);
                    }
                }
            }

            products = products.Skip(page * count).Take(count).ToList();
            if (products.Count != 0) return new ApiResult("Ok", HttpStatusCode.Accepted, products);
            else return new ApiResult("Products not found", HttpStatusCode.NotFound);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, (HttpStatusCode)500);
        }
    }
    public async Task<ApiResult> GetProductAsync(Guid id)
    {
        try
        {
            Product product = await _repository.GetProductAsync(id);
            if (product != null) return new ApiResult("Ok", HttpStatusCode.Accepted, product);
            else return new ApiResult("Product not found", HttpStatusCode.NotFound);
        }
        catch (Exception e)
        {
            return new ApiResult(e.Message, (HttpStatusCode)500);
        }
    }
}