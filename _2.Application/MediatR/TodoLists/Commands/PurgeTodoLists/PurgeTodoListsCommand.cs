using Application.Common.Interfaces;
using Application.Common.Security;
using MediatR;

namespace Application.MediatR.TodoLists.Commands.PurgeTodoLists;

[Authorize(Roles = "Administrator")]
[Authorize(Policy = "CanPurge")]
public record PurgeTodoListsCommand : IRequest;

public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeTodoListsCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public PurgeTodoListsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
    {
        _unitOfWork.TodoListRepository.Purge();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
