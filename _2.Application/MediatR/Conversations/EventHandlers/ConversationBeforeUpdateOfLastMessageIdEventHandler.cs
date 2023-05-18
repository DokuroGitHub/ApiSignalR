using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Conversations.EventHandlers;

public class ConversationBeforeUpdateOfLastMessageIdEventHandler : INotificationHandler<ConversationBeforeUpdateOfLastMessageIdEvent>
{
    private readonly ILogger<ConversationBeforeUpdateOfLastMessageIdEventHandler> _logger;

    public ConversationBeforeUpdateOfLastMessageIdEventHandler(ILogger<ConversationBeforeUpdateOfLastMessageIdEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ConversationBeforeUpdateOfLastMessageIdEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
