using AutoMapper;
using Core.Models;
using Infrastructure.DTO.ProductTO;

namespace Infrastructure.AutoMappers;

public class ProductAutoMapper : Profile
{
    public ProductAutoMapper()
    {
        CreateMap<CreateProductTO, Product>();
        CreateMap<UpdateProductTO, Product>();
    }
}