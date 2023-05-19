using FluentValidation;

namespace Application.MediatR.MessageAttachments.Commands.CreateMessageAttachment;

public class CreateMessageAttachmentCommandValidator : AbstractValidator<CreateMessageAttachmentCommand>
{
    public CreateMessageAttachmentCommandValidator()
    {
        RuleFor(x => x.MessageId)
            .NotNull().GreaterThan(0);
        RuleFor(x => x.FileUrl)
            .MaximumLength(400);
        RuleFor(x => x.ThumbUrl)
            .MaximumLength(400);
        RuleFor(x => x.Type)
            .IsInEnum();
    }
}
