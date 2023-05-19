using Application.Common.Interfaces;
using Application.Hubs.Conversations;
using Application.Hubs.Messages;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Conversations.EventHandlers;

public class ConversationAfterUpdateOfLastMessageIdEventHandler : INotificationHandler<ConversationAfterUpdateOfLastMessageIdEvent>
{
    private readonly ILogger<ConversationBeforeUpdateOfLastMessageIdEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<ConversationsHub> _conversationsHub;

    public ConversationAfterUpdateOfLastMessageIdEventHandler(
        ILogger<ConversationBeforeUpdateOfLastMessageIdEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<ConversationsHub> conversationsHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _conversationsHub = conversationsHub;
    }

    public async Task Handle(ConversationAfterUpdateOfLastMessageIdEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var conversationTask = Task.Run(async () =>
        {
            var message = await _unitOfWork.MessageRepository.SingleAsync<MessageBriefDto>(
                where: x => x.Id == notification.Item.LastMessageId,
                cancellationToken: cancellationToken);
            await _conversationsHub.Clients
                .Group($"Conversation_Id_{notification.Item.Id}")
                .SendAsync("ConversationUpdated_NewMessage", message, cancellationToken);
        });
        await Task.WhenAll(conversationTask);
    }
}
