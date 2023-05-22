using FluentValidation;

namespace Application.MediatR.MessageEmotes.Commands.UpdateMessageEmote;

public class UpdateMessageEmoteCommandValidator : AbstractValidator<UpdateMessageEmoteCommand>
{
    public UpdateMessageEmoteCommandValidator()
    {
        RuleFor(x => x.MessageId)
            .NotNull().GreaterThan(0);
        RuleFor(x => x.UserId)
            .NotNull().GreaterThan(0);
        RuleFor(x => x.Code)
            .IsInEnum();
    }
}
