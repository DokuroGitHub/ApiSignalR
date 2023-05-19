using FluentValidation;

namespace Application.MediatR.Messages.Commands.UpdateMessage;

public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
{
    public UpdateMessageCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
        RuleFor(x => x.Content)
            .MaximumLength(400).WithMessage("Content must not exceed 400 characters.");
    }
}
