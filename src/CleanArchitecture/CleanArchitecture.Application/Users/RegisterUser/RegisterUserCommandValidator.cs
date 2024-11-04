using FluentValidation;

namespace CleanArchitecture.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("The name can't be null.");
        RuleFor(c => c.LastNames).NotEmpty().WithMessage("The lastnames can't be null.");
        RuleFor(c => c.Email).EmailAddress();
        RuleFor(c => c.Password).NotEmpty().MinimumLength(5);
    }
}