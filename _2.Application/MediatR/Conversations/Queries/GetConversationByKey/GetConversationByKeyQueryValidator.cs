using FluentValidation;

namespace Application.MediatR.Conversations.Queries.GetConversationByKey;

public class GetConversationByKeyQueryValidator : AbstractValidator<GetConversationByKeyQuery>
{
    public GetConversationByKeyQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
