using FluentValidation;
using Infrastructure.DTO.ProductTO;

namespace Infrastructure.Validators.ProductValidators;

public class CreateProductTOValidator: AbstractValidator<CreateProductTO>
{
    public CreateProductTOValidator() 
    {
        RuleFor(m => m.Name).NotEmpty().WithMessage("Error: empty field \"Name\"");
        RuleFor(m => m.Price).NotEmpty().GreaterThanOrEqualTo(0)
            .WithMessage("Error: incorrect price");
        //Categories, SomeData, PhotoUrl - can be null
    }
}