using FluentValidation;

namespace Application.MediatR.Participants.Queries.GetPagedParticipants;

public class GetPagedParticipantsQueryValidator : AbstractValidator<GetPagedParticipantsQuery>
{
    public GetPagedParticipantsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
