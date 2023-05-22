using Application.Common.Interfaces;
using Application.Hubs.MessageAttachments;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.MessageAttachments.EventHandlers;

public class MessageAttachmentAfterInsertEventHandler : INotificationHandler<MessageAttachmentAfterInsertEvent>
{
    private readonly ILogger<MessageAttachmentAfterInsertEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<MessageAttachmentsHub> _conversationsHub;

    public MessageAttachmentAfterInsertEventHandler(
        ILogger<MessageAttachmentAfterInsertEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<MessageAttachmentsHub> conversationsHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _conversationsHub = conversationsHub;
    }

    public async Task Handle(MessageAttachmentAfterInsertEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var conversations = await _unitOfWork.MessageAttachmentRepository.GetAllAsync<MessageAttachmentBriefDto>(cancellationToken: cancellationToken);
        var conversationsTask = _conversationsHub.Clients
            .Group("MessageAttachmentsChanged")
            .SendAsync($"MessageAttachments_Id_{notification.Item.Id}_Inserted", conversations, cancellationToken);
        var conversationTask = _conversationsHub.Clients
            .Group($"MessageAttachment_Id_{notification.Item.Id}")
            .SendAsync("MessageAttachmentInserted", notification.Item, cancellationToken);
        await Task.WhenAll(conversationsTask, conversationTask);
    }
}
