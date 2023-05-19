using Application.Common.Interfaces;
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

    public MessageAfterInsertEventHandler(
        ILogger<MessageAfterInsertEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<MessagesHub> messagesHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _messagesHub = messagesHub;
    }

    public async Task Handle(MessageAfterInsertEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var messages = await _unitOfWork.MessageRepository.GetAllAsync<MessageBriefDto>(cancellationToken: cancellationToken);
        var messagesTask = _messagesHub.Clients
            .Group("MessagesChanged")
            .SendAsync($"Messages_Id_{notification.Item.Id}_Inserted", messages, cancellationToken);
        var messageTask = _messagesHub.Clients
            .Group($"Message_Id_{notification.Item.Id}")
            .SendAsync("MessageInserted", notification.Item, cancellationToken);
        // Conversation: set LastMessageId + ConversationAfterUpdateEvent
        var conversationTask = Task.Run(async () =>
        {
            // set LastMessageId
            var conversation = await _unitOfWork.ConversationRepository.SingleAsync(
                where: x => x.Id == notification.Item.ConversationId,
                cancellationToken,
                tracked: true);
            conversation.LastMessageId = notification.Item.Id;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            // ConversationAfterUpdateEvent
            conversation.AddDomainEvent(new ConversationBeforeUpdateOfLastMessageIdEvent(conversation));
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        });
        await Task.WhenAll(messagesTask, messageTask, conversationTask);
    }
}
