using FluentValidation;

namespace Application.MediatR.MessageEmotes.Queries.GetMessageEmoteByKey;

public class GetMessageEmoteByKeyQueryValidator : AbstractValidator<GetMessageEmoteByKeyQuery>
{
    public GetMessageEmoteByKeyQueryValidator()
    {
        RuleFor(x => x.MessageId)
            .NotNull().GreaterThan(0);
        RuleFor(x => x.UserId)
            .NotNull().GreaterThan(0);
    }
}
