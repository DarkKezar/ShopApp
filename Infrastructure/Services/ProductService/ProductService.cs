using AutoMapper;
using Core.Models;
using Core.Repositories.ProductRepository;
using FluentValidation.Results;
using Infrastructure.AutoMappers;
using Infrastructure.DTO.ProductTO;
using Infrastructure.Validators.ProductValidators;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Extensions;

namespace Infrastructure.Services.ProductService;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IActionResult> CreateProductAsync(CreateProductTO model)
    {
        ValidationResult result = await (new CreateProductTOValidator()).ValidateAsync(model);
        if (result.IsValid)
        {
            Product product = _mapper.Map<Product>(model);
            product = await _repository.CreatProductAsync(product);
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
            Product product = _mapper.Map<Product>(model);
            
            product = await _repository.UpdateProductAsync(product);
            return new OkResult();
        }
        else
        {
            return new ObjectResult(result.Errors);
        }
    }

    public async Task<IActionResult> DeleteProductAsync(DeleteProductTO model)
    {
        ValidationResult result = await (new DeleteProductToValidator()).ValidateAsync(model);
        if (result.IsValid)
        {
            await _repository.DeleteProductAsync(model.Id);
            return new OkResult();
        }
        else
        {
            return new ObjectResult(result.Errors);
        }
    }

    public async Task<IActionResult> GetAllProductsAsync(int count, int page)
    {
        IEnumerable<Product> products = (await _repository.GetAllProductsAsync()).Pagination(count, page);
        return new ObjectResult(new GetProductTO()
            { Products = products.ToList() });
    }

    public async Task<IActionResult> GetProductAsync(Guid id)
    {
        return new ObjectResult(await _repository.GeProductAsync(id));
    }
}