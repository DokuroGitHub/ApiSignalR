using Application.Common.Interfaces;
using Application.Hubs.Conversations;
using Application.MediatR.Conversations.Queries.GetPagedConversations;
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

        // /Conversations/AllConversations/ConversationsChanged
        var allConversationsHubTask = Task.Run(async () =>
        {
            var items = await _unitOfWork.ConversationRepository.GetAllAsync<ConversationBriefDto>(cancellationToken: cancellationToken);
            await _conversationsHub.Clients
                .Group("AllConversations")
                .SendAsync("ConversationsChanged", items, cancellationToken);
        });

        // /Conversations/Conversations/ConversationInserted
        var conversationsHubTask = _conversationsHub.Clients
            .Group("Conversations")
            .SendAsync("ConversationInserted", notification.Item, cancellationToken);

        await Task.WhenAll(
            allConversationsHubTask,
            conversationsHubTask);
    }
}
