using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Conversations.EventHandlers;

public class ConversationBeforeUpdateEventHandler : INotificationHandler<ConversationBeforeUpdateEvent>
{
    private readonly ILogger<ConversationBeforeUpdateEventHandler> _logger;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public ConversationBeforeUpdateEventHandler(
        ILogger<ConversationBeforeUpdateEventHandler> logger,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
    }

    public Task Handle(ConversationBeforeUpdateEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        notification.Item.UpdatedAt = _dateTimeService.Now;
        notification.Item.UpdatedBy = _currentUserService.UserId ?? 1;
        return Task.CompletedTask;
    }
}
