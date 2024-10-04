using Domain.Requests;
using FluentValidation;

namespace Application.Validations;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.");

        // TODO: Check if password is correctly formatted
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}