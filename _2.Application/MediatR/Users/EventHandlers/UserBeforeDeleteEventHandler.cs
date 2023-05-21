using Application.Common.Interfaces;
using Application.Hubs.Users;
using Domain.Events.Users;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Users.EventHandlers;

public class UserBeforeDeleteEventHandler : INotificationHandler<UserBeforeDeleteEvent>
{
    private readonly ILogger<UserBeforeDeleteEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<UsersHub> _usersHub;

    public UserBeforeDeleteEventHandler(
        ILogger<UserBeforeDeleteEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<UsersHub> usersHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _usersHub = usersHub;
    }

    public async Task Handle(UserBeforeDeleteEvent notification, CancellationToken cancellationToken)
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
