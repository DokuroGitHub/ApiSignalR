using FluentValidation;

namespace Application.MediatR.Messages.Commands.CreateMessage;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(x => x.ConversationId)
            .NotNull().GreaterThan(0);
        RuleFor(x => x.Content)
            .MaximumLength(400).WithMessage("Content must not exceed 400 characters.");
        RuleFor(x => x.ReplyTo)
            .GreaterThan(0);
    }
}
