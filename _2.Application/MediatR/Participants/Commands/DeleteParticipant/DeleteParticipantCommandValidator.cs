using FluentValidation;

namespace Application.MediatR.Participants.Commands.DeleteParticipant;

public class DeleteParticipantCommandValidator : AbstractValidator<DeleteParticipantCommand>
{
    public DeleteParticipantCommandValidator()
    {
        RuleFor(x => x.ConversationId)
            .NotNull().GreaterThan(0);
        RuleFor(x => x.UserId)
            .NotNull().GreaterThan(0);
    }
}
