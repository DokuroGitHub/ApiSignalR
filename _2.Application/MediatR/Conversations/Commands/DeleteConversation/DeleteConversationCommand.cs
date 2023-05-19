using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.MediatR.Conversations.Commands.DeleteConversation;

public record DeleteConversationCommand(int Id) : IRequest;

public class DeleteConversationCommandHandler : IRequestHandler<DeleteConversationCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteConversationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteConversationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.ConversationRepository.FindAsync(
                cancellationToken: cancellationToken,
                keyValues: request.Id);

        if (entity is null)
            throw new NotFoundException(nameof(Conversation), request.Id);

        _unitOfWork.ConversationRepository.Remove(entity);
        entity.AddDomainEvent(new ConversationBeforeDeleteEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new ConversationAfterDeleteEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
