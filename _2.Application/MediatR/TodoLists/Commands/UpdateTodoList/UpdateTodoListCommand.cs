using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.TodoLists.Commands.UpdateTodoList;

public record UpdateTodoListCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }
}

public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTodoListCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.TodoListRepository
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(TodoList), request.Id);
        }

        entity.Title = request.Title;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
