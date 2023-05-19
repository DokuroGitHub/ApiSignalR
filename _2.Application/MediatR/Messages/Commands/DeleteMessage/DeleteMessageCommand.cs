using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.MediatR.Messages.Commands.DeleteMessage;

public record DeleteMessageCommand(int Id) : IRequest;

public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMessageCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.MessageRepository.FindAsync(
                cancellationToken: cancellationToken,
                keyValues: request.Id);

        if (entity is null)
            throw new NotFoundException(nameof(Message), request.Id);

        _unitOfWork.MessageRepository.Remove(entity);
        entity.AddDomainEvent(new MessageBeforeDeleteEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new MessageAfterDeleteEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
