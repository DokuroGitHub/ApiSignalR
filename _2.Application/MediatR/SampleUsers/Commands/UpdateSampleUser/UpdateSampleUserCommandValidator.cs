using FluentValidation;

namespace Application.MediatR.SampleUsers.Commands.UpdateSampleUser;

public class UpdateSampleUserCommandValidator : AbstractValidator<UpdateSampleUserCommand>
{
    public UpdateSampleUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
        RuleFor(x => x.FirstName)
            .MaximumLength(20);
        RuleFor(x => x.LastName)
            .MaximumLength(20);
    }
}
