using FluentValidation;

namespace BDA.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Email).EmailAddress().NotEmpty();
        RuleFor(c => c.Password).NotEmpty();
    }
}