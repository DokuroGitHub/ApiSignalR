using Domain.Events.SampleUsers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.MediatR.SampleUsers.EventHandlers;

public class SampleUserBeforeUpdateOfPasswordEventHandler : INotificationHandler<SampleUserBeforeUpdateOfPasswordEvent>
{
    private readonly ILogger<SampleUserBeforeUpdateOfPasswordEventHandler> _logger;

    public SampleUserBeforeUpdateOfPasswordEventHandler(ILogger<SampleUserBeforeUpdateOfPasswordEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(SampleUserBeforeUpdateOfPasswordEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
