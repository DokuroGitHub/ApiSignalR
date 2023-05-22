using Application.Common.Interfaces;
using Application.Hubs.Conversations;
using Application.MediatR.Conversations.Queries.GetConversations;
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

        // /Conversations/AllConversations/ConversationsChanged
        var allConversationsHubTask = Task.Run(async () =>
        {
            var items = await _unitOfWork.ConversationRepository.GetAllAsync<ConversationBriefDto>(cancellationToken: cancellationToken);
            await _conversationsHub.Clients
                .Group("AllConversations")
                .SendAsync("ConversationsChanged", items, cancellationToken);
        });

        // /Conversations/Conversations/ConversationUpdated
        var conversationsHubTask = _conversationsHub.Clients
            .Group("Conversations")
            .SendAsync("ConversationUpdated", notification.Item, cancellationToken);

        // /Conversations/Conversation_Id_1_ConversationUpdated
        var conversationHubTask = _conversationsHub.Clients
            .Group($"Conversation_Id_{notification.Item.Id}")
            .SendAsync("ConversationUpdated", notification.Item, cancellationToken);

        await Task.WhenAll(
            allConversationsHubTask,
            conversationsHubTask,
            conversationHubTask);
    }
}
