using FluentValidation;

namespace Application.MediatR.Messages.Queries.GetPagedMessages;

public class GetPagedMessagesQueryValidator : AbstractValidator<GetPagedMessagesQuery>
{
    public GetPagedMessagesQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
