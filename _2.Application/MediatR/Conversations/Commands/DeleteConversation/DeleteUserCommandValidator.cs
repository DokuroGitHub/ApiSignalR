using FluentValidation;

namespace Application.MediatR.Conversations.Commands.DeleteConversation;

public class DeleteConversationCommandValidator : AbstractValidator<DeleteConversationCommand>
{
    public DeleteConversationCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
