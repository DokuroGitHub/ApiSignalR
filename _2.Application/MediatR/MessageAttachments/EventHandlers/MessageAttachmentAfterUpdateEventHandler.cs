using Application.Common.Interfaces;
using Application.Hubs.MessageAttachments;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.MessageAttachments.EventHandlers;

public partial class MessageAttachmentAfterUpdateEventHandler : INotificationHandler<MessageAttachmentAfterUpdateEvent>
{
    private readonly ILogger<MessageAttachmentAfterUpdateEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<MessageAttachmentsHub> _conversationsHub;

    public MessageAttachmentAfterUpdateEventHandler(
        ILogger<MessageAttachmentAfterUpdateEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<MessageAttachmentsHub> conversationsHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _conversationsHub = conversationsHub;
    }

    public async Task Handle(MessageAttachmentAfterUpdateEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var conversations = await _unitOfWork.MessageAttachmentRepository.GetAllAsync<MessageAttachmentBriefDto>(cancellationToken: cancellationToken);
        var conversationsTask = _conversationsHub.Clients
            .Group("MessageAttachmentsChanged")
            .SendAsync($"MessageAttachments_Id_{notification.Item.Id}_Updated", conversations, cancellationToken);
        var conversationTask = _conversationsHub.Clients
            .Group($"MessageAttachment_Id_{notification.Item.Id}")
            .SendAsync("MessageAttachmentUpdated", notification.Item, cancellationToken);
        await Task.WhenAll(conversationsTask, conversationTask);
    }
}
