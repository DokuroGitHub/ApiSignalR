using Application.Common.Interfaces;
using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.MessageAttachments.EventHandlers;

public class MessageAttachmentBeforeUpdateEventHandler : INotificationHandler<MessageAttachmentBeforeUpdateEvent>
{
    private readonly ILogger<MessageAttachmentBeforeUpdateEventHandler> _logger;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;

    public MessageAttachmentBeforeUpdateEventHandler(
        ILogger<MessageAttachmentBeforeUpdateEventHandler> logger,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
    }

    public Task Handle(MessageAttachmentBeforeUpdateEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
