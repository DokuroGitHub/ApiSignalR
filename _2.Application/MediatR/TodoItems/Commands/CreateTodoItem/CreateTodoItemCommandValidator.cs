using FluentValidation;

namespace Application.MediatR.TodoItems.Commands.CreateTodoItem;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(200)
            .NotEmpty();
    }
}
