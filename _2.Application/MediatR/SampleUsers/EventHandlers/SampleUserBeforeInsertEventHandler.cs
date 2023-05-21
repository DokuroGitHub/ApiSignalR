using Application.Common.Interfaces;
using Domain.Events.SampleUsers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.SampleUsers.EventHandlers;

public class SampleUserBeforeInsertEventHandler : INotificationHandler<SampleUserBeforeInsertEvent>
{
    private readonly ILogger<SampleUserBeforeInsertEventHandler> _logger;
    private readonly ICurrentUserService _currentUserService;

    public SampleUserBeforeInsertEventHandler(
        ILogger<SampleUserBeforeInsertEventHandler> logger,
        ICurrentUserService currentSampleUserService)
    {
        _logger = logger;
        _currentUserService = currentSampleUserService;
    }

    public Task Handle(SampleUserBeforeInsertEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        // notification.Item.CreatedAt = _dateTimeService.Now; // no need bc of default value
        notification.Item.CreatedBy = _currentUserService.UserId ?? 0;
        return Task.CompletedTask;
    }
}
