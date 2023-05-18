using Application.MediatR.Conversations.Commands.CreateConversation;
using Application.MediatR.Conversations.Commands.DeleteConversation;
using Application.MediatR.Conversations.Commands.UpdateConversation;
using Application.MediatR.Conversations.Queries.GetConversationByKey;
using Application.MediatR.Conversations.Queries.GetConversations;
using Application.MediatR.Conversations.Queries.GetPagedConversations;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1_1;

[ApiVersion("1.1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ConversationsController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPagedConversations([FromQuery] GetPagedConversationsQuery query)
    {
        var result = await Mediator.Send(query);
        Response.Headers.Add("X-Pagination", result.ToString());
        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
        => Ok(await Mediator.Send(new GetConversationsQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByKey(int id)
    => Ok(await Mediator.Send(new GetConversationByKeyQuery() { Id = id }));

    [HttpPost]
    public async Task<ActionResult> Create(CreateConversationCommand command)
    => Ok(await Mediator.Send(command));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateConversationCommand command)
    {
        command.Id = id;

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] JsonPatchDocument<UpdateConversationCommand> patchDocument)
    {
        var command = new UpdateConversationCommand();
        patchDocument.ApplyTo(command);
        command.Id = id;

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteConversationCommand(id));

        return NoContent();
    }
}
