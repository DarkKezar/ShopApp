using FluentValidation;
using Infrastructure.DTO.AccountTO;

namespace Infrastructure.Validators.AccountValidators;

public class CreateAccountTOValidator : AbstractValidator<CreateAccountTO>
{
    public CreateAccountTOValidator()
    {
        RuleFor(m => m.Name).NotEmpty().WithMessage("Error: empty field \"Name\"");
        RuleFor(m => m.Login).NotEmpty().WithMessage("Error: empty field \"Login\"");
        RuleFor(m => m.Password).NotEmpty().MinimumLength(8)
            .WithMessage("Error: incorrect password");
    }
}