using Application.Common.Interfaces;
using Application.Hubs.Conversations;
using Application.Hubs.Messages;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Messages.EventHandlers;

public class MessageAfterInsertEventHandler : INotificationHandler<MessageAfterInsertEvent>
{
    private readonly ILogger<MessageAfterInsertEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<MessagesHub> _messagesHub;
    private readonly IHubContext<ConversationsHub> _conversationsHub;

    public MessageAfterInsertEventHandler(
        ILogger<MessageAfterInsertEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<MessagesHub> messagesHub,
        IHubContext<ConversationsHub> conversationsHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _messagesHub = messagesHub;
        _conversationsHub = conversationsHub;
    }

    public async Task Handle(MessageAfterInsertEvent notification, CancellationToken cancellationToken)
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

        // /Messages/Messages/MessageInserted
        var messagesHubTask = _messagesHub.Clients
            .Group("Messages")
            .SendAsync("MessageInserted", notification.Item, cancellationToken);

        // ConversationBeforeUpdateOfLastMessageIdEvent
        var conversationTask = Task.Run(async () =>
        {
            // set LastMessageId
            var conversation = await _unitOfWork.ConversationRepository.SingleAsync(
                where: x => x.Id == notification.Item.ConversationId,
                cancellationToken,
                tracked: true);
            conversation.LastMessageId = notification.Item.Id;
            conversation.AddDomainEvent(new ConversationBeforeUpdateOfLastMessageIdEvent(conversation));
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        });

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

        // /Conversations/Conversation_Id_1_Messages/MessageInserted
        var conversationMessagesHubTask = _conversationsHub.Clients
            .Group($"Conversation_Id_{notification.Item.ConversationId}_Messages")
            .SendAsync("MessageInserted", notification.Item, cancellationToken);

        await Task.WhenAll(
            allMessagesHubTask,
            messagesHubTask,
            conversationTask,
            conversationAllMessagesHubTask,
            conversationMessagesHubTask);
    }
}
