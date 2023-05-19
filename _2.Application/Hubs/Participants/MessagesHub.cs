using Application.MediatR.Participants.Commands.CreateParticipant;
using Application.MediatR.Participants.Commands.DeleteParticipant;
using Application.MediatR.Participants.Commands.UpdateParticipant;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs.Participants;

public class ParticipantsHub : Hub // treat this as a Controller
{
    private ISender _mediator;

    public ParticipantsHub(ISender mediator)
    {
        _mediator = mediator;
    }

    public async Task JoinGroup(string groupName)
    {
        var addToGroupTask = Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        var sendTask = Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} joined {groupName}");
        await Task.WhenAll(addToGroupTask, sendTask);
    }

    public Task Create(CreateParticipantCommand command)
    => _mediator.Send(command);

    public async Task Update(
        int conversationId,
        int userId,
        UpdateParticipantCommand command)
    {
        command.ConversationId = conversationId;
        command.UserId = userId;
        await _mediator.Send(command);
    }

    public Task Delete(int conversationid, int userId)
    => _mediator.Send(new DeleteParticipantCommand(conversationid, userId));
}
