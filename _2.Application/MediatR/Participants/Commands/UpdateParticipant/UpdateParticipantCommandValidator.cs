using FluentValidation;

namespace Application.MediatR.Participants.Commands.UpdateParticipant;

public class UpdateParticipantCommandValidator : AbstractValidator<UpdateParticipantCommand>
{
    public UpdateParticipantCommandValidator()
    {
        RuleFor(x => x.ConversationId)
            .NotNull().GreaterThan(0);
        RuleFor(x => x.UserId)
            .NotNull().GreaterThan(0);
        RuleFor(x => x.Nickname)
            .MaximumLength(400);
        RuleFor(x => x.Role)
            .IsInEnum();
    }
}
