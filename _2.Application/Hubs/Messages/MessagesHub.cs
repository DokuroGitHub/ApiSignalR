using Application.MediatR.Messages.Commands.CreateMessage;
using Application.MediatR.Messages.Commands.DeleteMessage;
using Application.MediatR.Messages.Commands.UpdateMessage;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs.Messages;

public class MessagesHub : Hub // treat this as a Controller
{
    private ISender _mediator;

    public MessagesHub(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task JoinGroup(string groupName)
    {
        var addToGroupTask = Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        var sendTask = Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} joined {groupName}");
        await Task.WhenAll(addToGroupTask, sendTask);
    }

    public Task Create(CreateMessageCommand command)
    => _mediator.Send(command);

    public async Task Update(int id, UpdateMessageCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
    }

    public Task Delete(int id)
    => _mediator.Send(new DeleteMessageCommand(id));
}
