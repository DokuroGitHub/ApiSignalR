using Application.MediatR.Participants.Commands.CreateParticipant;
using Application.MediatR.Participants.Commands.DeleteParticipant;
using Application.MediatR.Participants.Commands.UpdateParticipant;
using Application.MediatR.Participants.Queries.GetParticipantByKey;
using Application.MediatR.Participants.Queries.GetParticipants;
using Application.MediatR.Participants.Queries.GetPagedParticipants;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1_1;

[ApiVersion("1.1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ParticipantsController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPagedParticipants([FromQuery] GetPagedParticipantsQuery query)
    {
        var result = await Mediator.Send(query);
        Response.Headers.Add("X-Pagination", result.ToString());
        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
        => Ok(await Mediator.Send(new GetParticipantsQuery()));

    [HttpGet("{conversationId}/{userId}")]
    public async Task<IActionResult> GetByKey(int conversationId, int userId)
    => Ok(await Mediator.Send(new GetParticipantByKeyQuery() { ConversationId = conversationId, UserId = userId }));

    [HttpPost]
    public async Task<ActionResult> Create(CreateParticipantCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{conversationId}/{userId}")]
    public async Task<IActionResult> Update(
        int conversationId, int userId, UpdateParticipantCommand command)
    {
        command.ConversationId = conversationId;
        command.UserId = userId;
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{conversationId}/{userId}")]
    public async Task<IActionResult> Update(
        int conversationId, int userId,
        [FromBody] JsonPatchDocument<UpdateParticipantCommand> patchDocument)
    {
        var command = new UpdateParticipantCommand();
        patchDocument.ApplyTo(command);
        command.ConversationId = conversationId;
        command.UserId = userId;
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{conversationId}/{userId}")]
    public async Task<IActionResult> Delete(int conversationId, int userId)
    {
        await Mediator.Send(new DeleteParticipantCommand(conversationId, userId));
        return NoContent();
    }
}
