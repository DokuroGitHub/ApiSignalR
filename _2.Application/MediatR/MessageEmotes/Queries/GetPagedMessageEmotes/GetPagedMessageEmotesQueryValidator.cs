using FluentValidation;

namespace Application.MediatR.MessageEmotes.Queries.GetPagedMessageEmotes;

public class GetPagedMessageEmotesQueryValidator : AbstractValidator<GetPagedMessageEmotesQuery>
{
    public GetPagedMessageEmotesQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
