using Application.Common.Interfaces;
using Application.Hubs.Conversations;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Conversations.EventHandlers;

public class ConversationAfterInsertEventHandler : INotificationHandler<ConversationAfterInsertEvent>
{
    private readonly ILogger<ConversationAfterInsertEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<ConversationsHub> _conversationsHub;

    public ConversationAfterInsertEventHandler(
        ILogger<ConversationAfterInsertEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<ConversationsHub> conversationsHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _conversationsHub = conversationsHub;
    }

    public async Task Handle(ConversationAfterInsertEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var conversations = await _unitOfWork.ConversationRepository.GetAllAsync<ConversationBriefDto>(cancellationToken: cancellationToken);
        var conversationsTask = _conversationsHub.Clients
            .Group("ConversationsChanged")
            .SendAsync($"Conversations_Id_{notification.Item.Id}_Inserted", conversations, cancellationToken);
        var conversationTask = _conversationsHub.Clients
            .Group($"Conversation_Id_{notification.Item.Id}")
            .SendAsync("ConversationInserted", notification.Item, cancellationToken);
        await Task.WhenAll(conversationsTask, conversationTask);
    }
}
