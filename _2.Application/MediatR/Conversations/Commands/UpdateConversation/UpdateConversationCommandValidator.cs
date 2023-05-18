using FluentValidation;

namespace Application.MediatR.Conversations.Commands.UpdateConversation;

public class UpdateConversationCommandValidator : AbstractValidator<UpdateConversationCommand>
{
    public UpdateConversationCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
        RuleFor(x => x.Title)
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
        RuleFor(x => x.Description)
            .MaximumLength(400).WithMessage("Description must not exceed 400 characters.");
        RuleFor(x => x.PhotoUrl)
            .MaximumLength(400).WithMessage("PhotoUrl must not exceed 400 characters.");
    }
}
