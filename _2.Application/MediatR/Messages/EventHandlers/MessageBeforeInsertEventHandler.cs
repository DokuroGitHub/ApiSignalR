using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Messages.EventHandlers;

public class MessageBeforeInsertEventHandler : INotificationHandler<MessageBeforeInsertEvent>
{
    private readonly ILogger<MessageBeforeInsertEventHandler> _logger;
    private readonly ICurrentUserService _currentUserService;

    public MessageBeforeInsertEventHandler(
        ILogger<MessageBeforeInsertEventHandler> logger,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Handle(MessageBeforeInsertEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        // set default values for Message
        var currentUserId = _currentUserService.UserId ?? 1;
        // notification.Item.CreatedAt = _dateTimeService.Now; // no need bc of default value
        notification.Item.CreatedBy = currentUserId;
        // set default values for Attachments
        foreach (var message in notification.Item.Attachments)
        {
        }
        return Task.CompletedTask;
    }
}
