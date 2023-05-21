using Application.Common.Interfaces;
using FluentValidation;

namespace Application.MediatR.Auth.Commands.LoginAnonymous;

public class LoginAnonymousCommandValidator : AbstractValidator<LoginAnonymousCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public LoginAnonymousCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
    }
}
