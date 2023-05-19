using FluentValidation;

namespace Application.MediatR.MessageAttachments.Queries.GetMessageAttachmentByKey;

public class GetMessageAttachmentByKeyQueryValidator : AbstractValidator<GetMessageAttachmentByKeyQuery>
{
    public GetMessageAttachmentByKeyQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
