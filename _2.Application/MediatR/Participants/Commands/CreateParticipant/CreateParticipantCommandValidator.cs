using FluentValidation;

namespace Application.MediatR.Participants.Commands.CreateParticipant;

public class CreateParticipantCommandValidator : AbstractValidator<CreateParticipantCommand>
{
    public CreateParticipantCommandValidator()
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
