using FluentValidation;

namespace Application.MediatR.MessageEmotes.Commands.DeleteMessageEmote;

public class DeleteMessageEmoteCommandValidator : AbstractValidator<DeleteMessageEmoteCommand>
{
    public DeleteMessageEmoteCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().GreaterThan(0);
    }
}
