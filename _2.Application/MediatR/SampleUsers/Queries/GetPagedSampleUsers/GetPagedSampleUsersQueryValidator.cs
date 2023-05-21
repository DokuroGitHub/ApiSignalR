using FluentValidation;

namespace Application.MediatR.SampleUsers.Queries.GetPagedSampleUsers;

public class GetPagedSampleUsersQueryValidator : AbstractValidator<GetPagedSampleUsersQuery>
{
    public GetPagedSampleUsersQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
