using Application.Common.Interfaces;
using FluentValidation;

namespace Application.MediatR.Auth.Commands.LoginThirdParty;

public class LoginThirdPartyCommandValidator : AbstractValidator<LoginThirdPartyCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public LoginThirdPartyCommandValidator(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MinimumLength(6).WithMessage("Email must not less than 6 characters.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
            .MustAsync(BeUnique).WithMessage("The specified Email already exists.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must not less than 6 characters.")
            .MaximumLength(100);
        RuleFor(x => x.PasswordConfirm)
            .Must((x, y) => string.IsNullOrEmpty(y) || x.Password == y).WithMessage("Password and Confirm Password must match.");
    }

    private Task<bool> BeUnique(string email, CancellationToken cancellationToken)
    => _unitOfWork.UserRepository.AllAsync(
        assert: x => x.Email != email && x.Username != email,
        cancellationToken: cancellationToken);
}
