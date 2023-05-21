using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events.Users;
using MediatR;

namespace Application.MediatR.Users.Commands.UpdateUserLastSeen;

public record UpdateUserLastSeenCommand : IRequest;

public class UpdateUserLastSeenCommandHandler : IRequestHandler<UpdateUserLastSeenCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserLastSeenCommandHandler(
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateUserLastSeenCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var entity = await _unitOfWork.UserRepository.SingleOrDefaultAsync(
            where: x => x.Id == userId,
            tracked: true,
            cancellationToken: cancellationToken);

        if (entity is null)
            throw new NotFoundException(nameof(User), "User not found");

        entity.LastSeen = DateTime.UtcNow;
        entity.AddDomainEvent(new UserBeforeUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new UserAfterUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
