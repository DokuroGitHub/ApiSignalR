using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.MessageAttachments.EventHandlers;

public class MessageAttachmentBeforeInsertEventHandler : INotificationHandler<MessageAttachmentBeforeInsertEvent>
{
    private readonly ILogger<MessageAttachmentBeforeInsertEventHandler> _logger;
    private readonly ICurrentUserService _currentUserService;

    public MessageAttachmentBeforeInsertEventHandler(
        ILogger<MessageAttachmentBeforeInsertEventHandler> logger,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Handle(MessageAttachmentBeforeInsertEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        // set default values for MessageAttachment
        return Task.CompletedTask;
    }
}
