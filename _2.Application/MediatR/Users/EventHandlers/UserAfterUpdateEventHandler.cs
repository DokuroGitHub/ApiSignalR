using Application.Common.Interfaces;
using Application.Hubs.Users;
using Domain.Events.Users;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Users.EventHandlers;

public partial class UserAfterUpdateEventHandler : INotificationHandler<UserAfterUpdateEvent>
{
    private readonly ILogger<UserAfterUpdateEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<UsersHub> _usersHub;

    public UserAfterUpdateEventHandler(
        ILogger<UserAfterUpdateEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<UsersHub> usersHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _usersHub = usersHub;
    }

    public async Task Handle(UserAfterUpdateEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var users = await _unitOfWork.UserRepository.GetAllAsync<UserBriefDto>(cancellationToken: cancellationToken);
        var usersTask = _usersHub.Clients
            .Group("UsersChanged")
            .SendAsync($"Users_Id_{notification.Item.Id}_Updated", users, cancellationToken);
        var userTask = _usersHub.Clients
            .Group($"User_Id_{notification.Item.Id}")
            .SendAsync("UserUpdated", notification.Item, cancellationToken);
        await Task.WhenAll(usersTask, userTask);
    }
}
