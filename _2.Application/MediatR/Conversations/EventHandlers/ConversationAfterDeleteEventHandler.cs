using Application.Common.Interfaces;
using Application.Hubs.Conversations;
using Application.MediatR.Conversations.Queries.GetConversations;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Conversations.EventHandlers;

public class ConversationAfterDeleteEventHandler : INotificationHandler<ConversationAfterDeleteEvent>
{
    private readonly ILogger<ConversationAfterDeleteEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<ConversationsHub> _conversationsHub;

    public ConversationAfterDeleteEventHandler(
        ILogger<ConversationAfterDeleteEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<ConversationsHub> conversationsHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _conversationsHub = conversationsHub;
    }

    // can not call this bc entity is deleted already
    public async Task Handle(ConversationAfterDeleteEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var conversations = await _unitOfWork.ConversationRepository.GetAllAsync<ConversationBriefDto>(cancellationToken: cancellationToken);
        var conversationsTask = _conversationsHub.Clients
            .Group("ConversationsChanged")
            .SendAsync($"Conversations_Id_{notification.Item.Id}_Deleted", conversations, cancellationToken);
        var conversationTask = _conversationsHub.Clients
            .Group($"Conversation_Id_{notification.Item.Id}")
            .SendAsync("ConversationDeleted", notification.Item, cancellationToken);
        await Task.WhenAll(conversationsTask, conversationTask);
    }
}
