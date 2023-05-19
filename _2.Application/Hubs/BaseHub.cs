using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs;

public abstract class BaseHub : Hub
{
    private ISender _mediator;

    public BaseHub(ISender mediator)
    {
        _mediator = mediator;
    }

    protected ISender Mediator => _mediator;

    public async Task JoinGroup(string groupName)
    {
        var addToGroupTask = Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        var sendTask = Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} joined {groupName}");
        await Task.WhenAll(addToGroupTask, sendTask);
    }
}