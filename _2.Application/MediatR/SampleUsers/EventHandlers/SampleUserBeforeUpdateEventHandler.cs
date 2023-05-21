using Application.Common.Interfaces;
using Domain.Events.SampleUsers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.SampleUsers.EventHandlers;

public class SampleUserBeforeUpdateEventHandler : INotificationHandler<SampleUserBeforeUpdateEvent>
{
    private readonly ILogger<SampleUserBeforeUpdateEventHandler> _logger;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public SampleUserBeforeUpdateEventHandler(
        ILogger<SampleUserBeforeUpdateEventHandler> logger,
        IDateTimeService dateTimeService,
        ICurrentUserService currentSampleUserService)
    {
        _logger = logger;
        _dateTimeService = dateTimeService;
        _currentUserService = currentSampleUserService;
    }

    public Task Handle(SampleUserBeforeUpdateEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        notification.Item.UpdatedAt = _dateTimeService.Now;
        notification.Item.UpdatedBy = _currentUserService.UserId ?? 0;
        return Task.CompletedTask;
    }
}
