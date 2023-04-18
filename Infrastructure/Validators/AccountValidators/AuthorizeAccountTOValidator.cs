using FluentValidation;
using Infrastructure.DTO.AccountTO;

namespace Infrastructure.Validators.AccountValidators;

public class AuthorizeAccountTOValidator : AbstractValidator<AuthorizeAccountTO>
{
    public AuthorizeAccountTOValidator()
    {
        RuleFor(m => m.Login).NotEmpty().WithMessage("Please, enter Login");
        RuleFor(m => m.Password).NotEmpty().MinimumLength(8)
            .WithMessage("Please, enter password");
    }
}