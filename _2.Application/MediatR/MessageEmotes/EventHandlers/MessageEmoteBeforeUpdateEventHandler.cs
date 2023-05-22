using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.MessageEmotes.EventHandlers;

public class MessageEmoteBeforeUpdateEventHandler : INotificationHandler<MessageEmoteBeforeUpdateEvent>
{
    private readonly ILogger<MessageEmoteBeforeUpdateEventHandler> _logger;

    public MessageEmoteBeforeUpdateEventHandler(
        ILogger<MessageEmoteBeforeUpdateEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MessageEmoteBeforeUpdateEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
