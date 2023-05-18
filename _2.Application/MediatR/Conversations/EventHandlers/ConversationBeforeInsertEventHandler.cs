using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Conversations.EventHandlers;

public class ConversationBeforeInsertEventHandler : INotificationHandler<ConversationBeforeInsertEvent>
{
    private readonly ILogger<ConversationBeforeInsertEventHandler> _logger;
    private readonly ICurrentUserService _currentUserService;

    public ConversationBeforeInsertEventHandler(
        ILogger<ConversationBeforeInsertEventHandler> logger,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Handle(ConversationBeforeInsertEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        // notification.Item.CreatedAt = _dateTimeService.Now; // no need bc of default value
        notification.Item.CreatedBy = _currentUserService.UserId ?? 1;
        return Task.CompletedTask;
    }
}
