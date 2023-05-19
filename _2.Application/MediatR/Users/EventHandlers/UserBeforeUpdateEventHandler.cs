using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Users.EventHandlers;

public class UserBeforeUpdateEventHandler : INotificationHandler<UserBeforeUpdateEvent>
{
    private readonly ILogger<UserBeforeUpdateEventHandler> _logger;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public UserBeforeUpdateEventHandler(
        ILogger<UserBeforeUpdateEventHandler> logger,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
    }

    public Task Handle(UserBeforeUpdateEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        notification.Item.UpdatedAt = _dateTimeService.Now;
        notification.Item.UpdatedBy = _currentUserService.UserId ?? 0;
        return Task.CompletedTask;
    }
}
