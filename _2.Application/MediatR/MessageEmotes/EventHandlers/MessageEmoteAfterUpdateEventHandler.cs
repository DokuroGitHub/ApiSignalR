using Application.Common.Interfaces;
using Application.Hubs.Conversations;
using Application.Hubs.MessageEmotes;
using Application.Hubs.Messages;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.MessageEmotes.EventHandlers;

public partial class MessageEmoteAfterUpdateEventHandler : INotificationHandler<MessageEmoteAfterUpdateEvent>
{
    private readonly ILogger<MessageEmoteAfterUpdateEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<MessageEmotesHub> _messageEmotesHub;
    private readonly IHubContext<MessagesHub> _messagesHub;
    private readonly IHubContext<ConversationsHub> _conversationsHub;

    public MessageEmoteAfterUpdateEventHandler(
        ILogger<MessageEmoteAfterUpdateEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<MessageEmotesHub> messageEmotesHub,
        IHubContext<MessagesHub> messagesHub,
        IHubContext<ConversationsHub> conversationsHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _messageEmotesHub = messageEmotesHub;
        _messagesHub = messagesHub;
        _conversationsHub = conversationsHub;
    }

    public async Task Handle(MessageEmoteAfterUpdateEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        // /MessageEmotes/AllMessageEmotes/MessageEmotesChanged
        var allMessageEmotesHubTask = Task.Run(async () =>
        {
            var items = await _unitOfWork.MessageEmoteRepository.GetAllAsync<MessageEmoteBriefDto>(cancellationToken: cancellationToken);
            await _messageEmotesHub.Clients
                .Group("AllMessageEmotes")
                .SendAsync("MessageEmotesChanged", items, cancellationToken);
        });

        // /MessageEmotes/MessageEmotes/MessageEmoteUpdated
        var messageEmotesHubTask = _messageEmotesHub.Clients
            .Group("MessageEmotes")
            .SendAsync("MessageEmoteUpdated", notification.Item, cancellationToken);

        // /MessageEmotes/MessageEmote_MessageId_1_UserId_1/MessageEmoteUpdated
        var messageEmoteHubTask = _messagesHub.Clients
            .Group($"MessageEmote_MessageId_{notification.Item.MessageId}_UserId_{notification.Item.UserId}")
            .SendAsync("MessageEmoteUpdated", notification.Item, cancellationToken);

        // /Messages/Message_Id_1_AllMessageEmotes/MessageEmotesChanged
        var messageAllMessageEmotesHubTask = Task.Run(async () =>
        {
            var items = await _unitOfWork.MessageEmoteRepository.GetAllAsync<MessageEmoteBriefDto>(
                where: x => x.MessageId == notification.Item.MessageId,
                cancellationToken: cancellationToken);
            await _messagesHub.Clients
                .Group($"Message_Id_{notification.Item.MessageId}_AllMessageEmotes")
                .SendAsync("MessageEmotesChanged", items, cancellationToken);
        });

        // /Messages/Message_Id_1_MessageEmotes/MessageEmoteUpdated
        var messageMessageEmotesHubTask = _messagesHub.Clients
            .Group($"Message_Id_{notification.Item.MessageId}_MessageEmotes")
            .SendAsync("MessageEmoteUpdated", notification.Item, cancellationToken);

        // /Conversations/Conversation_Id_1_Message_Id_1_AllMessageEmotes/MessageEmotesChanged
        var conversationMessageAllMessageEmotesHubTask = Task.Run(async () =>
        {
            var message = await _unitOfWork.MessageRepository.SingleAsync<MessageBriefDto>(
                where: x => x.Id == notification.Item.MessageId,
                cancellationToken: cancellationToken);
            var messageEmotes = await _unitOfWork.MessageEmoteRepository.GetAllAsync<MessageEmoteBriefDto>(
                where: x => x.MessageId == notification.Item.MessageId,
                cancellationToken: cancellationToken);
            await _conversationsHub.Clients
                .Group($"Conversation_Id_{message.ConversationId}_Message_Id_{message.Id}_AllMessageEmotes")
                .SendAsync("MessageEmotesChanged", messageEmotes, cancellationToken);
        });

        // /Conversations/Conversation_Id_1_Message_Id_1_MessageEmotes/MessageEmoteUpdated
        var conversationMessageMessageEmotesHubTask = Task.Run(async () =>
        {
            var message = await _unitOfWork.MessageRepository.SingleAsync<MessageBriefDto>(
                where: x => x.Id == notification.Item.MessageId,
                cancellationToken: cancellationToken);
            await _conversationsHub.Clients
             .Group($"Conversation_Id_{message.ConversationId}_Message_Id_{message.Id}_MessageEmotes")
             .SendAsync("MessageEmoteUpdated", notification.Item, cancellationToken);
        });

        await Task.WhenAll(
            allMessageEmotesHubTask,
            messageEmotesHubTask,
            messageEmoteHubTask,
            messageAllMessageEmotesHubTask,
            messageMessageEmotesHubTask,
            conversationMessageAllMessageEmotesHubTask,
            conversationMessageMessageEmotesHubTask);
    }
}
