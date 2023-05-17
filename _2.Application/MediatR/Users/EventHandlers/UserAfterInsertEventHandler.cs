using Application.Common.Interfaces;
using Application.Hubs.Users;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Users.EventHandlers;

public class UserAfterInsertEventHandler : INotificationHandler<UserAfterInsertEvent>
{
    private readonly ILogger<UserAfterInsertEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<UsersHub> _usersHub;

    public UserAfterInsertEventHandler(
        ILogger<UserAfterInsertEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<UsersHub> usersHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _usersHub = usersHub;
    }

    public async Task Handle(UserAfterInsertEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var users = await _unitOfWork.UserRepository.GetAllAsync<UserBriefDto>(cancellationToken: cancellationToken);
        var usersTask = _usersHub.Clients
            .Group("UsersChanged")
            .SendAsync($"Users_Id_{notification.Item.Id}_Inserted", users, cancellationToken);
        var userTask = _usersHub.Clients
            .Group($"User_Id_{notification.Item.Id}")
            .SendAsync("UserInserted", notification.Item, cancellationToken);
        await Task.WhenAll(usersTask, userTask);
    }
}
