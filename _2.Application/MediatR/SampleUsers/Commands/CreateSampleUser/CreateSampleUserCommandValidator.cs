using Application.Common.Interfaces;
using FluentValidation;

namespace Application.MediatR.SampleUsers.Commands.CreateSampleUser;

public class CreateSampleUserCommandValidator : AbstractValidator<CreateSampleUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSampleUserCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.FirstName)
            .MaximumLength(20);
        RuleFor(x => x.LastName)
            .MaximumLength(20);
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MinimumLength(6).WithMessage("Email must not less than 6 characters.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.")
            .MustAsync(BeUniqueSampleUsername).WithMessage("The specified Email already exists.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must not less than 6 characters.")
            .MaximumLength(100).WithMessage("Password must not exceed 100 characters.");
        RuleFor(x => x.SampleUsername)
            .NotEmpty().WithMessage("SampleUsername is required.")
            .MinimumLength(6).WithMessage("SampleUsername must not less than 6 characters.")
            .MaximumLength(100).WithMessage("SampleUsername must not exceed 100 characters.")
            .MustAsync(BeUniqueSampleUsername).WithMessage("The specified SampleUsername already exists.");
    }

    private async Task<bool> BeUniqueSampleUsername(string username, CancellationToken cancellationToken)
    {
        return await _unitOfWork.SampleUserRepository.AllAsync(
            assert: x => x.Username != username,
            cancellationToken: cancellationToken);
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _unitOfWork.SampleUserRepository.AllAsync(
            assert: x => x.Email != email,
            cancellationToken: cancellationToken);
    }
}
