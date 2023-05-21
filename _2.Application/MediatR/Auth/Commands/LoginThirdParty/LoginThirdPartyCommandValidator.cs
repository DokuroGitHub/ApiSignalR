using FluentValidation;

namespace Application.MediatR.Auth.Commands.LoginThirdParty;

public class LoginThirdPartyCommandValidator : AbstractValidator<LoginThirdPartyCommand>
{
    public LoginThirdPartyCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MinimumLength(6).WithMessage("Email must not less than 6 characters.")
            .MaximumLength(50).WithMessage("Email must not exceed 50 characters.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must not less than 6 characters.")
            .MaximumLength(50);
        RuleFor(x => x.PasswordConfirm)
            .Must((x, y) => string.IsNullOrEmpty(y) || x.Password == y).WithMessage("Password and Confirm Password must match.");
    }
}
