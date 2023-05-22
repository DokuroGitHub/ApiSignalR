using Application.Common.Interfaces;
using Application.Hubs.Conversations;
using Application.Hubs.MessageEmotes;
using Application.Hubs.Messages;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.MessageEmotes.EventHandlers;

public class MessageEmoteAfterDeleteEventHandler : INotificationHandler<MessageEmoteAfterDeleteEvent>
{
    private readonly ILogger<MessageEmoteAfterDeleteEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<MessageEmotesHub> _messageEmotesHub;
    private readonly IHubContext<MessagesHub> _messagesHub;
    private readonly IHubContext<ConversationsHub> _conversationsHub;

    public MessageEmoteAfterDeleteEventHandler(
        ILogger<MessageEmoteAfterDeleteEventHandler> logger,
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

    // can not call this bc entity is deleted already
    public async Task Handle(MessageEmoteAfterDeleteEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        // /MessageEmotes/AllMessageEmotes/MessageEmotesChanged
        var allMessageEmotesHubTask = Task.Run(async () =>
        {
            var items = await _unitOfWork.MessageEmoteRepository.GetAllAsync<MessageEmoteBriefDto>(
                cancellationToken: cancellationToken);
            await _messageEmotesHub.Clients
                .Group("AllMessageEmotes")
                .SendAsync("MessageEmotesChanged", items, cancellationToken);
        });

        // /MessageEmotes/MessageEmotes/MessageEmoteDeleted
        var messageEmotesHubTask = _messageEmotesHub.Clients
            .Group("MessageEmotes")
            .SendAsync("MessageEmoteDeleted", notification.Item, cancellationToken);

        // /MessageEmotes/MessageEmote_MessageId_1_UserId_1/MessageEmoteDeleted
        var messageEmoteHubTask = _messagesHub.Clients
            .Group($"MessageEmote_MessageId_{notification.Item.MessageId}_UserId_{notification.Item.UserId}")
            .SendAsync("MessageEmoteDeleted", notification.Item, cancellationToken);

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

        // /Messages/Message_Id_1_MessageEmotes/MessageEmoteDeleted
        var messageMessageEmotesHubTask = _messagesHub.Clients
            .Group($"Message_Id_{notification.Item.MessageId}_MessageEmotes")
            .SendAsync("MessageEmoteDeleted", notification.Item, cancellationToken);

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

        // /Conversations/Conversation_Id_1_Message_Id_1_MessageEmotes/MessageEmoteDeleted
        var conversationMessageMessageEmotesHubTask = Task.Run(async () =>
        {
            var message = await _unitOfWork.MessageRepository.SingleAsync<MessageBriefDto>(
                where: x => x.Id == notification.Item.MessageId,
                cancellationToken: cancellationToken);
            await _conversationsHub.Clients
                .Group($"Conversation_Id_{message.ConversationId}_Message_Id_{message.Id}_MessageEmotes")
                .SendAsync("MessageEmoteDeleted", notification.Item, cancellationToken);
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
