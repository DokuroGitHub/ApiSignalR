using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Messages.EventHandlers;

public class MessageBeforeUpdateEventHandler : INotificationHandler<MessageBeforeUpdateEvent>
{
    private readonly ILogger<MessageBeforeUpdateEventHandler> _logger;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public MessageBeforeUpdateEventHandler(
        ILogger<MessageBeforeUpdateEventHandler> logger,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
    }

    public Task Handle(MessageBeforeUpdateEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        notification.Item.UpdatedAt = _dateTimeService.Now;
        return Task.CompletedTask;
    }
}
