using Application.Common.Interfaces;
using Application.Hubs.Messages;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Messages.EventHandlers;

public class MessageAfterDeleteEventHandler : INotificationHandler<MessageAfterDeleteEvent>
{
    private readonly ILogger<MessageAfterDeleteEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<MessagesHub> _messagesHub;

    public MessageAfterDeleteEventHandler(
        ILogger<MessageAfterDeleteEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<MessagesHub> messagesHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _messagesHub = messagesHub;
    }

    // can not call this bc entity is deleted already
    public async Task Handle(MessageAfterDeleteEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var messages = await _unitOfWork.MessageRepository.GetAllAsync<MessageBriefDto>(cancellationToken: cancellationToken);
        var messagesTask = _messagesHub.Clients
            .Group("MessagesChanged")
            .SendAsync($"Messages_Id_{notification.Item.Id}_Deleted", messages, cancellationToken);
        var messageTask = _messagesHub.Clients
            .Group($"Message_Id_{notification.Item.Id}")
            .SendAsync("MessageDeleted", notification.Item, cancellationToken);
        await Task.WhenAll(messagesTask, messageTask);
    }
}
