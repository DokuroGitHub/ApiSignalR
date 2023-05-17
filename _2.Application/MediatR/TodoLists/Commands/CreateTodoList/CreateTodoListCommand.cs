using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.TodoLists.Commands.CreateTodoList;

public record CreateTodoListCommand : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTodoListCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoList();

        entity.Title = request.Title;

        _unitOfWork.TodoListRepository.Add(entity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
