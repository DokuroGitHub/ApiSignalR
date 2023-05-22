using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.MediatR.MessageEmotes.Commands.DeleteMessageEmote;

public record DeleteMessageEmoteCommand(int Id) : IRequest;

public class DeleteMessageEmoteCommandHandler : IRequestHandler<DeleteMessageEmoteCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMessageEmoteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteMessageEmoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.MessageEmoteRepository.FindAsync(
                cancellationToken: cancellationToken,
                keyValues: request.Id);

        if (entity is null)
            throw new NotFoundException(nameof(MessageEmote), request.Id);

        _unitOfWork.MessageEmoteRepository.Remove(entity);
        entity.AddDomainEvent(new MessageEmoteBeforeDeleteEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new MessageEmoteAfterDeleteEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
