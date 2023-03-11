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

    public async Task<IActionResult> CreateProductAsync(CreateProductTO model)
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
            catch (NpgsqlException e)
            {
                return new ObjectResult(e);
            }
            return new ObjectResult(product);
        }
        else
        {
            return new ObjectResult(result.Errors);
        }
    }

    public async Task<IActionResult> UpdateProductAsync(UpdateProductTO model)
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
                /* Add if
                product.Price = model.NewData.Price;
                product.Categories = (await _categoryRepository.GetAllCategoriesAsync())
                    .Where(c => model.NewData.CategoriesId.Contains(c.Id)).ToList();
                */
                product = await _repository.UpdateProductAsync(product);
                return new OkObjectResult(product);
            }
            catch (NpgsqlException e)
            {
                return new ObjectResult(e);
            }
        }
        else
        {
            return new BadRequestObjectResult(result.Errors);
        }
    }

    public async Task<IActionResult> DeleteProductAsync(DeleteProductTO model)
    {
        ValidationResult result = await (new DeleteProductToValidator()).ValidateAsync(model);
        if (result.IsValid)
        {
            try
            {
                await _repository.DeleteProductAsync(model.Id);
            }
            catch (NpgsqlException e)
            {
                return new ObjectResult(e);
            }
            return new OkResult();
        }
        else
        {
            return new BadRequestObjectResult(result.Errors);
        }
    }

    public async Task<IActionResult> GetAllProductsAsync(int count, int page)
    {
        try
        {
            List<Product> products = (await _repository.GetAllProductsAsync())
                .Include(p => p.Categories)
                .Pagination(count, page).ToList();
            return new OkObjectResult(products);
            /*
            return new OkObjectResult(new GetProductTO()
                { Products = products });
                */
        }
        catch (NpgsqlException e)
        {
            return new ObjectResult(e);
        }
    }
    public async Task<IActionResult> GetAllProductsAsync(List<Guid> categoriesId, int count, int page)
    {
        try
        {
            List<Category> categories = (await _categoryRepository.GetAllCategoriesAsync())
                .Where(c => categoriesId.Contains(c.Id))
                .Include(c => c.Products)
                .ThenInclude(p => p.ProductStats)
                .ToList();
            if (categories.Count == 0) return new NotFoundResult();

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
            if (products.Count != 0) return new OkObjectResult(products);
            else return new NotFoundResult();
        }
        catch (NpgsqlException e)
        {
            return new ObjectResult(e);
        }
    }
    public async Task<IActionResult> GetProductAsync(Guid id)
    {
        try
        {
            Product product = await _repository.GetProductAsync(id);
            if (product != null) return new OkObjectResult(product);
            else return new NotFoundResult();
        }
        catch (NpgsqlException e)
        {
            return new ObjectResult(e);
        }
    }
}