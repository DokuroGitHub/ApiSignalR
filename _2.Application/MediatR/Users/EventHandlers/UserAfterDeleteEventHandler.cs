using Application.Common.Interfaces;
using Application.Hubs.Users;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Users.EventHandlers;

public class UserAfterDeleteEventHandler : INotificationHandler<UserAfterDeleteEvent>
{
    private readonly ILogger<UserAfterDeleteEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<UsersHub> _usersHub;

    public UserAfterDeleteEventHandler(
        ILogger<UserAfterDeleteEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<UsersHub> usersHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _usersHub = usersHub;
    }

    // can not call this bc entity is deleted already
    public async Task Handle(UserAfterDeleteEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var users = await _unitOfWork.UserRepository.GetAllAsync<UserBriefDto>(cancellationToken: cancellationToken);
        var usersTask = _usersHub.Clients
            .Group("UsersChanged")
            .SendAsync($"Users_Id_{notification.Item.Id}_Deleted", users, cancellationToken);
        var userTask = _usersHub.Clients
            .Group($"User_Id_{notification.Item.Id}")
            .SendAsync("UserDeleted", notification.Item, cancellationToken);
        await Task.WhenAll(usersTask, userTask);
    }
}
