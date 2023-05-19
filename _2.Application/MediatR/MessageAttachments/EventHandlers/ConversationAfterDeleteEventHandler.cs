using Application.Common.Interfaces;
using Application.Hubs.MessageAttachments;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.MessageAttachments.EventHandlers;

public class MessageAttachmentAfterDeleteEventHandler : INotificationHandler<MessageAttachmentAfterDeleteEvent>
{
    private readonly ILogger<MessageAttachmentAfterDeleteEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<MessageAttachmentsHub> _conversationsHub;

    public MessageAttachmentAfterDeleteEventHandler(
        ILogger<MessageAttachmentAfterDeleteEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<MessageAttachmentsHub> conversationsHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _conversationsHub = conversationsHub;
    }

    // can not call this bc entity is deleted already
    public async Task Handle(MessageAttachmentAfterDeleteEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var conversations = await _unitOfWork.MessageAttachmentRepository.GetAllAsync<MessageAttachmentBriefDto>(cancellationToken: cancellationToken);
        var conversationsTask = _conversationsHub.Clients
            .Group("MessageAttachmentsChanged")
            .SendAsync($"MessageAttachments_Id_{notification.Item.Id}_Deleted", conversations, cancellationToken);
        var conversationTask = _conversationsHub.Clients
            .Group($"MessageAttachment_Id_{notification.Item.Id}")
            .SendAsync("MessageAttachmentDeleted", notification.Item, cancellationToken);
        await Task.WhenAll(conversationsTask, conversationTask);
    }
}
