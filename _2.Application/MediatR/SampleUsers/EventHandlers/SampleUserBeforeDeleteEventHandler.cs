using Application.Common.Interfaces;
using Application.Hubs.SampleUsers;
using Domain.Events.SampleUsers;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.SampleUsers.EventHandlers;

public class SampleUserBeforeDeleteEventHandler : INotificationHandler<SampleUserBeforeDeleteEvent>
{
    private readonly ILogger<SampleUserBeforeDeleteEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<SampleUsersHub> _usersHub;

    public SampleUserBeforeDeleteEventHandler(
        ILogger<SampleUserBeforeDeleteEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<SampleUsersHub> usersHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _usersHub = usersHub;
    }

    public async Task Handle(SampleUserBeforeDeleteEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var users = await _unitOfWork.SampleUserRepository.GetAllAsync<SampleUserBriefDto>(cancellationToken: cancellationToken);
        var usersTask = _usersHub.Clients
            .Group("SampleUsersChanged")
            .SendAsync($"SampleUsers_Id_{notification.Item.Id}_Deleted", users, cancellationToken);
        var userTask = _usersHub.Clients
            .Group($"SampleUser_Id_{notification.Item.Id}")
            .SendAsync("SampleUserDeleted", notification.Item, cancellationToken);
        await Task.WhenAll(usersTask, userTask);
    }
}
