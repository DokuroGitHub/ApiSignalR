using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.SampleUsers;
using MediatR;

namespace Application.MediatR.SampleUsers.Commands.DeleteSampleUser;

public record DeleteSampleUserCommand(int Id) : IRequest;

public class DeleteSampleUserCommandHandler : IRequestHandler<DeleteSampleUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSampleUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteSampleUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.SampleUserRepository.FindAsync(
                cancellationToken: cancellationToken,
                keyValues: request.Id);

        if (entity is null)
            throw new NotFoundException(nameof(SampleUser), request.Id);

        _unitOfWork.SampleUserRepository.Remove(entity);
        entity.AddDomainEvent(new SampleUserBeforeDeleteEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new SampleUserAfterDeleteEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
