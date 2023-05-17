using Application.Common.Interfaces;
using FluentValidation;

namespace Application.MediatR.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTodoListCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _unitOfWork.TodoListRepository.AllAsync(
            assert: x => x.Title != title,
            cancellationToken: cancellationToken);
    }
}
