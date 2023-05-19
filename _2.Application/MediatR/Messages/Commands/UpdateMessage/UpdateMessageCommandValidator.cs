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
