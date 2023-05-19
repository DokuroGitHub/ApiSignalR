using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.MediatR.Participants.Commands.DeleteParticipant;

public record DeleteParticipantCommand(int ConversationId, int UserId) : IRequest;

public class DeleteParticipantCommandHandler : IRequestHandler<DeleteParticipantCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteParticipantCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteParticipantCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.ParticipantRepository.FindAsync(
                cancellationToken: cancellationToken,
                keyValues: new { request.ConversationId, request.UserId });

        if (entity is null)
            throw new NotFoundException(nameof(Participant), new { request.ConversationId, request.UserId });

        _unitOfWork.ParticipantRepository.Remove(entity);
        entity.AddDomainEvent(new ParticipantBeforeDeleteEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new ParticipantAfterDeleteEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
