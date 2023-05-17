using FluentValidation;

namespace Application.MediatR.Users.Queries.GetUserByKey;

public class GetUserByKeyQueryValidator : AbstractValidator<GetUserByKeyQuery>
{
    public GetUserByKeyQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
