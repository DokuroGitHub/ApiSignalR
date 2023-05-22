using Application.Common.Interfaces;
using Application.Hubs.MessageAttachments;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.MessageAttachments.EventHandlers;

public class MessageAttachmentBeforeDeleteEventHandler : INotificationHandler<MessageAttachmentBeforeDeleteEvent>
{
    private readonly ILogger<MessageAttachmentBeforeDeleteEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<MessageAttachmentsHub> _conversationsHub;

    public MessageAttachmentBeforeDeleteEventHandler(
        ILogger<MessageAttachmentBeforeDeleteEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<MessageAttachmentsHub> conversationsHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _conversationsHub = conversationsHub;
    }

    public async Task Handle(MessageAttachmentBeforeDeleteEvent notification, CancellationToken cancellationToken)
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
