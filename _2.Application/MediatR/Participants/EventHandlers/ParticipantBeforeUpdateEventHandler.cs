using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Participants.EventHandlers;

public class ParticipantBeforeUpdateEventHandler : INotificationHandler<ParticipantBeforeUpdateEvent>
{
    private readonly ILogger<ParticipantBeforeUpdateEventHandler> _logger;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public ParticipantBeforeUpdateEventHandler(
        ILogger<ParticipantBeforeUpdateEventHandler> logger,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
    }

    public Task Handle(ParticipantBeforeUpdateEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        notification.Item.UpdatedAt = _dateTimeService.Now;
        return Task.CompletedTask;
    }
}
