using FluentValidation;

namespace Application.MediatR.Messages.Queries.GetMessageByKey;

public class GetMessageByKeyQueryValidator : AbstractValidator<GetMessageByKeyQuery>
{
    public GetMessageByKeyQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
