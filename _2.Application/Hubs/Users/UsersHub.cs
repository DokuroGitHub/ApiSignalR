using Application.MediatR.Users.Commands.CreateUser;
using Application.MediatR.Users.Commands.DeleteUser;
using Application.MediatR.Users.Commands.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs.Users;

public class UsersHub : Hub // treat this as a Controller
{
    private ISender _mediator;

    public UsersHub(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task JoinGroup(string groupName)
    {
        var addToGroupTask = Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        var sendTask = Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} joined {groupName}");
        await Task.WhenAll(addToGroupTask, sendTask);
    }

    public Task Create(CreateUserCommand command)
    => _mediator.Send(command);

    public async Task Update(int id, UpdateUserCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
    }

    public Task Delete(int id)
    => _mediator.Send(new DeleteUserCommand(id));
}
