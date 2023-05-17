using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.TodoLists.Commands.DeleteTodoList;

public record DeleteTodoListCommand(int Id) : IRequest;

public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTodoListCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.TodoListRepository.SingleOrDefaultAsync(
            where: x => x.Id == request.Id,
            cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(TodoList), request.Id);
        }

        _unitOfWork.TodoListRepository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
