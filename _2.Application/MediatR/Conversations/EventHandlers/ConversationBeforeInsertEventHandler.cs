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
        // set default values for Conversation
        var currentUserId = _currentUserService.UserId ?? 1;
        // notification.Item.CreatedAt = _dateTimeService.Now; // no need bc of default value
        notification.Item.CreatedBy = currentUserId;
        // set default values for Messages
        foreach (var message in notification.Item.Messages)
        {
            message.CreatedBy = currentUserId;
            // set default values for MessageAttachments
        }
        // set default values for Invitations
        foreach (var conversationInvitation in notification.Item.Invitations)
        {
            conversationInvitation.CreatedBy = currentUserId;
        }
        // set default values for Participants
        foreach (var participant in notification.Item.Participants)
        {
            participant.CreatedBy = currentUserId;
        }
        return Task.CompletedTask;
    }
}
