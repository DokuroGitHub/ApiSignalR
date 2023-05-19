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
        RuleForEach(x => x.Attachments)
            .ChildRules(x =>
            {
                x.RuleFor(x => x.FileUrl)
                    .MaximumLength(400).WithMessage("FileUrl must not exceed 400 characters.");
                x.RuleFor(x => x.ThumbUrl)
                    .MaximumLength(400).WithMessage("ThumbUrl must not exceed 400 characters.");
                x.RuleFor(x => x.Type)
                    .IsInEnum();
            });
    }
}
