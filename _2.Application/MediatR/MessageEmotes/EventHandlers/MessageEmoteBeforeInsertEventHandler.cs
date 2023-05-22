using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.MessageEmotes.EventHandlers;

public class MessageEmoteBeforeInsertEventHandler : INotificationHandler<MessageEmoteBeforeInsertEvent>
{
    private readonly ILogger<MessageEmoteBeforeInsertEventHandler> _logger;

    public MessageEmoteBeforeInsertEventHandler(
        ILogger<MessageEmoteBeforeInsertEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MessageEmoteBeforeInsertEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        // set default values for MessageEmote
        return Task.CompletedTask;
    }
}
