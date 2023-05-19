using FluentValidation;

namespace Application.MediatR.Participants.Queries.GetParticipantByKey;

public class GetParticipantByKeyQueryValidator : AbstractValidator<GetParticipantByKeyQuery>
{
    public GetParticipantByKeyQueryValidator()
    {
        RuleFor(x => x.ConversationId)
            .NotNull().GreaterThan(0);
        RuleFor(x => x.UserId)
            .NotNull().GreaterThan(0);
    }
}
