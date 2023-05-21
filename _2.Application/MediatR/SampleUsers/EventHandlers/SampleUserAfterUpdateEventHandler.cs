using Application.Common.Interfaces;
using Application.Hubs.SampleUsers;
using Domain.Events.SampleUsers;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.SampleUsers.EventHandlers;

public partial class SampleUserAfterUpdateEventHandler : INotificationHandler<SampleUserAfterUpdateEvent>
{
    private readonly ILogger<SampleUserAfterUpdateEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<SampleUsersHub> _usersHub;

    public SampleUserAfterUpdateEventHandler(
        ILogger<SampleUserAfterUpdateEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<SampleUsersHub> usersHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _usersHub = usersHub;
    }

    public async Task Handle(SampleUserAfterUpdateEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var users = await _unitOfWork.SampleUserRepository.GetAllAsync<SampleUserBriefDto>(cancellationToken: cancellationToken);
        var usersTask = _usersHub.Clients
            .Group("SampleUsersChanged")
            .SendAsync($"SampleUsers_Id_{notification.Item.Id}_Updated", users, cancellationToken);
        var userTask = _usersHub.Clients
            .Group($"SampleUser_Id_{notification.Item.Id}")
            .SendAsync("SampleUserUpdated", notification.Item, cancellationToken);
        await Task.WhenAll(usersTask, userTask);
    }
}
