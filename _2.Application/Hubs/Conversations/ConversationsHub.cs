using Application.MediatR.Conversations.Commands.CreateConversation;
using Application.MediatR.Conversations.Commands.DeleteConversation;
using Application.MediatR.Conversations.Commands.UpdateConversation;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs.Conversations;

public class ConversationsHub : Hub // treat this as a Controller
{
    private ISender _mediator;

    public ConversationsHub(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task JoinGroup(string groupName)
    {
        var addToGroupTask = Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        var sendTask = Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} joined {groupName}");
        await Task.WhenAll(addToGroupTask, sendTask);
    }

    public Task Create(CreateConversationCommand command)
    => _mediator.Send(command);

    public async Task Update(int id, UpdateConversationCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
    }

    public Task Delete(int id)
    => _mediator.Send(new DeleteConversationCommand(id));
}
