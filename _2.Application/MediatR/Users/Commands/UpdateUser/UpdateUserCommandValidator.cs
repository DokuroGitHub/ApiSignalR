using FluentValidation;

namespace Application.MediatR.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
        RuleFor(x => x.FirstName)
            .MaximumLength(20);
        RuleFor(x => x.LastName)
            .MaximumLength(20);
    }
}
