using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.MediatR.TodoItems.Commands.DeleteTodoItem;

public record DeleteTodoItemCommand(int Id) : IRequest;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTodoItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.TodoItemRepository
            .SingleOrDefaultAsync(
                where: x => x.Id == request.Id,
                cancellationToken: cancellationToken);

        if (entity is null)
            throw new NotFoundException(nameof(TodoItem), request.Id);

        _unitOfWork.TodoItemRepository.Remove(entity);
        entity.AddDomainEvent(new TodoItemDeletedEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

}
