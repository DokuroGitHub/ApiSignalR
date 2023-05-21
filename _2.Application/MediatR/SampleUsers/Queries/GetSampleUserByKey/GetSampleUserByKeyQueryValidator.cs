using FluentValidation;

namespace Application.MediatR.SampleUsers.Queries.GetSampleUserByKey;

public class GetSampleUserByKeyQueryValidator : AbstractValidator<GetSampleUserByKeyQuery>
{
    public GetSampleUserByKeyQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
