using FluentValidation;

namespace Application.MediatR.Auth.Queries.RegisterThirdParty;

public class LoginThirtPartyQueryValidator : AbstractValidator<RegisterThirdPartyQuery>
{
    public LoginThirtPartyQueryValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(6).WithMessage("Username must not less than 6 characters.")
            .MaximumLength(100).WithMessage("Username must not exceed 100 characters.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must not less than 6 characters.")
            .MaximumLength(100).WithMessage("Password must not exceed 100 characters.");
        RuleFor(x => x.PasswordConfirm)
            .Must((x, y) => string.IsNullOrEmpty(y) || x.Password == y).WithMessage("Password and Confirm Password must match.");
    }
}
