using FluentValidation;

namespace Application.MediatR.MessageEmotes.Commands.CreateMessageEmote;

public class CreateMessageEmoteCommandValidator : AbstractValidator<CreateMessageEmoteCommand>
{
    public CreateMessageEmoteCommandValidator()
    {
        RuleFor(x => x.MessageId)
            .NotNull().GreaterThan(0);
        RuleFor(x => x.FileUrl)
            .MaximumLength(400);
        RuleFor(x => x.ThumbUrl)
            .MaximumLength(400);
        RuleFor(x => x.Type)
            .IsInEnum();
    }
}
