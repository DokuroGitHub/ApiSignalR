using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Participants.EventHandlers;

public class ParticipantBeforeInsertEventHandler : INotificationHandler<ParticipantBeforeInsertEvent>
{
    private readonly ILogger<ParticipantBeforeInsertEventHandler> _logger;
    private readonly ICurrentUserService _currentUserService;

    public ParticipantBeforeInsertEventHandler(
        ILogger<ParticipantBeforeInsertEventHandler> logger,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Handle(ParticipantBeforeInsertEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        // set default values for Participant
        var currentUserId = _currentUserService.UserId ?? 0;
        // notification.Item.CreatedAt = _dateTimeService.Now; // no need bc of default value
        notification.Item.CreatedBy = currentUserId;
        return Task.CompletedTask;
    }
}
