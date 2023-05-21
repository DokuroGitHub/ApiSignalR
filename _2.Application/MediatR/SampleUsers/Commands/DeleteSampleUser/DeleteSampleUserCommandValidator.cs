using FluentValidation;

namespace Application.MediatR.SampleUsers.Commands.DeleteSampleUser;

public class DeleteSampleUserCommandValidator : AbstractValidator<DeleteSampleUserCommand>
{
    public DeleteSampleUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
