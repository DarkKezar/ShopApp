using FluentValidation;
using Infrastructure.DTO.ProductTO;

namespace Infrastructure.Validators.ProductValidators;

public class DeleteProductToValidator : AbstractValidator<DeleteProductTO>
{
    public DeleteProductToValidator()
    {
        RuleFor(m => m.Id).NotEmpty().WithMessage("Error: empty field \"Id\"");
    }
}