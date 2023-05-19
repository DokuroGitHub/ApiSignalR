using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs.DeletedMessages;

public class DeletedMessagesHub : Hub // treat this as a Controller
{
    private ISender _mediator;

    public DeletedMessagesHub(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task JoinGroup(string groupName)
    {
        var addToGroupTask = Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        var sendTask = Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} joined {groupName}");
        await Task.WhenAll(addToGroupTask, sendTask);
    }
}
