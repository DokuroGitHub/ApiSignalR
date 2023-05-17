using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.Users.EventHandlers;

public class UserBeforeUpdateOfPasswordEventHandler : INotificationHandler<UserBeforeUpdateOfPasswordEvent>
{
    private readonly ILogger<UserBeforeUpdateOfPasswordEventHandler> _logger;

    public UserBeforeUpdateOfPasswordEventHandler(ILogger<UserBeforeUpdateOfPasswordEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserBeforeUpdateOfPasswordEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
