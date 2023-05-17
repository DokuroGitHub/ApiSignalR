using FluentValidation;

namespace Application.MediatR.Auth.Queries.LoginAdmin;

public class LoginAdminQueryValidator : AbstractValidator<LoginAdminQuery>
{
    public LoginAdminQueryValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(5).WithMessage("Username must not less than 5 characters.")
            .MaximumLength(20).WithMessage("Username must not exceed 20 characters.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(5).WithMessage("Password must not less than 5 characters.")
            .MaximumLength(20);
        RuleFor(x => x.PasswordConfirm)
            .Must((x, y) => string.IsNullOrEmpty(y) || x.Password == y).WithMessage("Password and Confirm Password must match.");
    }
}
