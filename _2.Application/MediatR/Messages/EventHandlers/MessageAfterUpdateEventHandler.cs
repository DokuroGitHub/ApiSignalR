using Application.Common.Interfaces;
using Application.Hubs.Messages;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Messages.EventHandlers;

public partial class MessageAfterUpdateEventHandler : INotificationHandler<MessageAfterUpdateEvent>
{
    private readonly ILogger<MessageAfterUpdateEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<MessagesHub> _messagesHub;

    public MessageAfterUpdateEventHandler(
        ILogger<MessageAfterUpdateEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<MessagesHub> messagesHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _messagesHub = messagesHub;
    }

    public async Task Handle(MessageAfterUpdateEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var messages = await _unitOfWork.MessageRepository.GetAllAsync<MessageBriefDto>(cancellationToken: cancellationToken);
        var messagesTask = _messagesHub.Clients
            .Group("MessagesChanged")
            .SendAsync($"Messages_Id_{notification.Item.Id}_Updated", messages, cancellationToken);
        var messageTask = _messagesHub.Clients
            .Group($"Message_Id_{notification.Item.Id}")
            .SendAsync("MessageUpdated", notification.Item, cancellationToken);
        await Task.WhenAll(messagesTask, messageTask);
    }
}
