using FluentValidation;

namespace Application.MediatR.MessageAttachments.Commands.DeleteMessageAttachment;

public class DeleteMessageAttachmentCommandValidator : AbstractValidator<DeleteMessageAttachmentCommand>
{
    public DeleteMessageAttachmentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().GreaterThan(0);
    }
}
