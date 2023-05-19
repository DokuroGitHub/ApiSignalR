using FluentValidation;

namespace Application.MediatR.MessageAttachments.Commands.UpdateMessageAttachment;

public class UpdateMessageAttachmentCommandValidator : AbstractValidator<UpdateMessageAttachmentCommand>
{
    public UpdateMessageAttachmentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().GreaterThan(0);
        RuleFor(x => x.FileUrl)
            .MaximumLength(400);
        RuleFor(x => x.ThumbUrl)
            .MaximumLength(400);
        RuleFor(x => x.Type)
            .IsInEnum();
    }
}
