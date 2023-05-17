using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.TodoItems.Commands.UpdateTodoItem;

public record UpdateTodoItemCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTodoItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.TodoItemRepository
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

        entity.Title = request.Title;
        entity.Done = request.Done; // event TodoItemCompletedEvent is added in this property's setter

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
