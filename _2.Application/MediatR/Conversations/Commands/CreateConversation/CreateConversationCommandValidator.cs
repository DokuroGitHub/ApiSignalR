using FluentValidation;

namespace Application.MediatR.Conversations.Commands.CreateConversation;

public class CreateConversationCommandValidator : AbstractValidator<CreateConversationCommand>
{
    public CreateConversationCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
        RuleFor(x => x.Description)
            .MaximumLength(400).WithMessage("Description must not exceed 400 characters.");
        RuleFor(x => x.PhotoUrl)
            .MaximumLength(400).WithMessage("PhotoUrl must not exceed 400 characters.");
    }
}
