using Application.Common.Interfaces;
using Application.Hubs.Conversations;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Conversations.EventHandlers;

public partial class ConversationAfterUpdateEventHandler : INotificationHandler<ConversationAfterUpdateEvent>
{
    private readonly ILogger<ConversationAfterUpdateEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<ConversationsHub> _conversationsHub;

    public ConversationAfterUpdateEventHandler(
        ILogger<ConversationAfterUpdateEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<ConversationsHub> conversationsHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _conversationsHub = conversationsHub;
    }

    public async Task Handle(ConversationAfterUpdateEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var conversations = await _unitOfWork.ConversationRepository.GetAllAsync<ConversationBriefDto>(cancellationToken: cancellationToken);
        var conversationsTask = _conversationsHub.Clients
            .Group("ConversationsChanged")
            // .All
            .SendAsync($"Conversations_Id_{notification.Item.Id}_Updated", conversations, cancellationToken);
        var conversationTask = _conversationsHub.Clients
            .Group($"Conversation_Id_{notification.Item.Id}")
            .SendAsync("ConversationUpdated", notification.Item, cancellationToken);
        await Task.WhenAll(conversationsTask, conversationTask);
    }
}
