using FluentValidation;

namespace Application.MediatR.Messages.Commands.DeleteMessage;

public class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
{
    public DeleteMessageCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
