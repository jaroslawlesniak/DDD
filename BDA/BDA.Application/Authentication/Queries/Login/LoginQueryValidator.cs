using FluentValidation;

namespace BDA.Application.Authentication.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(c => c.Email).NotEmpty().WithMessage("Email is required.");
        RuleFor(c => c.Password).NotEmpty().WithMessage("Password is required.");
    }
}