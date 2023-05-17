using Application.Common.Interfaces;
using FluentValidation;

namespace Application.MediatR.TodoLists.Commands.UpdateTodoList;

public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTodoListCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    public async Task<bool> BeUniqueTitle(UpdateTodoListCommand model, string title, CancellationToken cancellationToken)
    {
        return await _unitOfWork.TodoListRepository.AllAsync(
            where: l => l.Id != model.Id,
            assert: l => l.Title != title,
            cancellationToken: cancellationToken);
    }
}
