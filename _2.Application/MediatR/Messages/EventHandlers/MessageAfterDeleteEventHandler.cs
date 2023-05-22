using Application.Common.Interfaces;
using Application.Hubs.Conversations;
using Application.Hubs.Messages;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Messages.EventHandlers;

public class MessageAfterDeleteEventHandler : INotificationHandler<MessageAfterDeleteEvent>
{
    private readonly ILogger<MessageAfterDeleteEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<MessagesHub> _messagesHub;
    private readonly IHubContext<ConversationsHub> _conversationsHub;

    public MessageAfterDeleteEventHandler(
        ILogger<MessageAfterDeleteEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<MessagesHub> messagesHub,
        IHubContext<ConversationsHub> conversationsHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _messagesHub = messagesHub;
        _conversationsHub = conversationsHub;
    }

    // can not call this bc entity is deleted already
    public async Task Handle(MessageAfterDeleteEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        // /Messages/AllMessages/MessagesChanged
        var allMessagesHubTask = Task.Run(async () =>
        {
            var messages = await _unitOfWork.MessageRepository.GetAllAsync<MessageBriefDto>(cancellationToken: cancellationToken);
            await _messagesHub.Clients
                .Group("AllMessages")
                .SendAsync("MessagesChanged", messages, cancellationToken);
        });

        // /Messages/Messages/MessageDeleted
        var messagesHubTask = _messagesHub.Clients
            .Group("Messages")
            .SendAsync("MessageDeleted", notification.Item, cancellationToken);

        // /Messages/Message_Id_1_MessageDeleted
        var messageHubTask = _messagesHub.Clients
            .Group($"Message_Id_{notification.Item.Id}")
            .SendAsync("MessageDeleted", notification.Item, cancellationToken);

        // /Conversations/Conversation_Id_1_AllMessages/MessagesChanged
        var conversationAllMessagesHubTask = Task.Run(async () =>
        {
            var messages = await _unitOfWork.MessageRepository.GetAllAsync<MessageBriefDto>(
                where: x => x.ConversationId == notification.Item.ConversationId,
                cancellationToken: cancellationToken);
            await _conversationsHub.Clients
                .Group($"Conversation_Id_{notification.Item.ConversationId}_AllMessages")
                .SendAsync("MessagesChanged", messages, cancellationToken);
        });

        // /Conversations/Conversation_Id_1_Messages/MessageDeleted
        var conversationMessagesHubTask = _conversationsHub.Clients
            .Group($"Conversation_Id_{notification.Item.ConversationId}_Messages")
            .SendAsync("MessageDeleted", notification.Item, cancellationToken);

        await Task.WhenAll(
            allMessagesHubTask,
            messagesHubTask,
            messageHubTask,
            conversationAllMessagesHubTask,
            conversationMessagesHubTask);
    }
}
