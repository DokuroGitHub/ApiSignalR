using Application.Common.Interfaces;
using Application.Hubs.Conversations;
using Application.Hubs.Messages;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Conversations.EventHandlers;

public class ConversationBeforeUpdateOfLastMessageIdEventHandler : INotificationHandler<ConversationBeforeUpdateOfLastMessageIdEvent>
{
    private readonly ILogger<ConversationBeforeUpdateOfLastMessageIdEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<ConversationsHub> _conversationsHub;

    public ConversationBeforeUpdateOfLastMessageIdEventHandler(
        ILogger<ConversationBeforeUpdateOfLastMessageIdEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<ConversationsHub> conversationsHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _conversationsHub = conversationsHub;
    }

    public async Task Handle(ConversationBeforeUpdateOfLastMessageIdEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var conversationTask = Task.Run(async () =>
        {

        });
        await Task.WhenAll(conversationTask);
    }
}
