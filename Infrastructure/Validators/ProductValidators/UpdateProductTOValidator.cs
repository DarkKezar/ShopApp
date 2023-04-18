using FluentValidation;
using Infrastructure.DTO.ProductTO;

namespace Infrastructure.Validators.ProductValidators;

public class UpdateProductTOValidator : AbstractValidator<UpdateProductTO>
{
    public UpdateProductTOValidator()
    {
        RuleFor(m => m.Id).NotEmpty().WithMessage("Error: empty field \"Id\"");
        RuleFor(m => m.NewData.Name).NotEmpty().WithMessage("Error: empty field \"Name\"");
        RuleFor(m => m.NewData.Price).NotEmpty().GreaterThan(0)
            .WithMessage("Error: incorrect price");
        //Categories, SomeData, PhotoUrl - can be null
    }
}