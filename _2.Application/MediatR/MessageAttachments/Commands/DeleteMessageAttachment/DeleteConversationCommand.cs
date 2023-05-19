using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.MediatR.MessageAttachments.Commands.DeleteMessageAttachment;

public record DeleteMessageAttachmentCommand(int Id) : IRequest;

public class DeleteMessageAttachmentCommandHandler : IRequestHandler<DeleteMessageAttachmentCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMessageAttachmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteMessageAttachmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.MessageAttachmentRepository.FindAsync(
                cancellationToken: cancellationToken,
                keyValues: request.Id);

        if (entity is null)
            throw new NotFoundException(nameof(MessageAttachment), request.Id);

        _unitOfWork.MessageAttachmentRepository.Remove(entity);
        entity.AddDomainEvent(new MessageAttachmentBeforeDeleteEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new MessageAttachmentAfterDeleteEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
