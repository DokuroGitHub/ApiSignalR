using FluentValidation;

namespace Application.MediatR.Auth.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(6).WithMessage("Username must not less than 6 characters.")
            .MaximumLength(20).WithMessage("Username must not exceed 20 characters.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must not less than 6 characters.")
            .MaximumLength(20);
        RuleFor(x => x.PasswordConfirm)
            .Must((x, y) => string.IsNullOrEmpty(y) || x.Password == y).WithMessage("Password and Confirm Password must match.");
    }
}
