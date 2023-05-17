using Application.Common.Interfaces;
using FluentValidation;

namespace Application.MediatR.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.FirstName)
            .MaximumLength(20);
        RuleFor(x => x.LastName)
            .MaximumLength(20);
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MinimumLength(6).WithMessage("Email must not less than 6 characters.")
            .MaximumLength(20).WithMessage("Email must not exceed 20 characters.")
            .MustAsync(BeUniqueUsername).WithMessage("The specified Email already exists.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must not less than 6 characters.")
            .MaximumLength(20);
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(6).WithMessage("Username must not less than 6 characters.")
            .MaximumLength(20).WithMessage("Username must not exceed 20 characters.")
            .MustAsync(BeUniqueUsername).WithMessage("The specified Username already exists.");
    }

    private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
    {
        return await _unitOfWork.UserRepository.AllAsync(
            assert: x => x.Username != username,
            cancellationToken: cancellationToken);
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _unitOfWork.UserRepository.AllAsync(
            assert: x => x.Email != email,
            cancellationToken: cancellationToken);
    }
}
