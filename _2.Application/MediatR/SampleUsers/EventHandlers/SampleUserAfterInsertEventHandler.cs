using Application.Common.Interfaces;
using Application.Hubs.SampleUsers;
using Domain.Events.SampleUsers;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.SampleUsers.EventHandlers;

public class SampleUserAfterInsertEventHandler : INotificationHandler<SampleUserAfterInsertEvent>
{
    private readonly ILogger<SampleUserAfterInsertEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHubContext<SampleUsersHub> _usersHub;

    public SampleUserAfterInsertEventHandler(
        ILogger<SampleUserAfterInsertEventHandler> logger,
        IUnitOfWork unitOfWork,
        IHubContext<SampleUsersHub> usersHub)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _usersHub = usersHub;
    }

    public async Task Handle(SampleUserAfterInsertEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        var users = await _unitOfWork.SampleUserRepository.GetAllAsync<SampleUserBriefDto>(cancellationToken: cancellationToken);
        var usersTask = _usersHub.Clients
            .Group("SampleUsersChanged")
            .SendAsync($"SampleUsers_Id_{notification.Item.Id}_Inserted", users, cancellationToken);
        var userTask = _usersHub.Clients
            .Group($"SampleUser_Id_{notification.Item.Id}")
            .SendAsync("SampleUserInserted", notification.Item, cancellationToken);
        await Task.WhenAll(usersTask, userTask);
    }
}
