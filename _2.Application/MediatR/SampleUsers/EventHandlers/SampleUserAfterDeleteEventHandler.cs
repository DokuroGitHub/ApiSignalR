using Application.Common.Interfaces;
using Application.Hubs.SampleUsers;
using Domain.Events.SampleUsers;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.SampleUsers.EventHandlers;

public class SampleUserAfterDeleteEventHandler : INotificationHandler<SampleUserAfterDeleteEvent>
{
    private readonly ILogger<SampleUserAfterDeleteEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<SampleUsersHub> _usersHub;

    public SampleUserAfterDeleteEventHandler(
        ILogger<SampleUserAfterDeleteEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<SampleUsersHub> usersHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _usersHub = usersHub;
    }

    // can not call this bc entity is deleted already
    public async Task Handle(SampleUserAfterDeleteEvent notification, CancellationToken cancellationToken)
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
