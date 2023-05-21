using Application.Common.Interfaces;
using Domain.Events.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Users.EventHandlers;

public class UserBeforeInsertEventHandler : INotificationHandler<UserBeforeInsertEvent>
{
    private readonly ILogger<UserBeforeInsertEventHandler> _logger;
    private readonly ICurrentUserService _currentUserService;

    public UserBeforeInsertEventHandler(
        ILogger<UserBeforeInsertEventHandler> logger,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Handle(UserBeforeInsertEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        // notification.Item.CreatedAt = _dateTimeService.Now; // no need bc of default value
        notification.Item.CreatedBy = _currentUserService.UserId ?? 1;
        return Task.CompletedTask;
    }
}
