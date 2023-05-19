using Application.Common.Interfaces;
using Application.Hubs.Participants;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Participants.EventHandlers;

public class ParticipantBeforeDeleteEventHandler : INotificationHandler<ParticipantBeforeDeleteEvent>
{
    private readonly ILogger<ParticipantBeforeDeleteEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<ParticipantsHub> _participantsHub;

    public ParticipantBeforeDeleteEventHandler(
        ILogger<ParticipantBeforeDeleteEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<ParticipantsHub> participantsHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _participantsHub = participantsHub;
    }

    public async Task Handle(ParticipantBeforeDeleteEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var participants = await _unitOfWork.ParticipantRepository.GetAllAsync<ParticipantBriefDto>(cancellationToken: cancellationToken);
        var participantsTask = _participantsHub.Clients
            .Group("ParticipantsChanged")
            .SendAsync(
                $"Participants_ConversationId_UserId_{notification.Item.ConversationId}_{notification.Item.UserId}_Deleted",
                participants, cancellationToken);
        var participantTask = _participantsHub.Clients
            .Group($"Participant_ConversationId_UserId_{notification.Item.ConversationId}_{notification.Item.UserId}")
            .SendAsync("ParticipantDeleted", notification.Item, cancellationToken);
        await Task.WhenAll(participantsTask, participantTask);
    }
}
