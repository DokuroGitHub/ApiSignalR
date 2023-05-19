using Application.MediatR.Messages.Commands.CreateMessage;
using Application.MediatR.Messages.Commands.DeleteMessage;
using Application.MediatR.Messages.Commands.UpdateMessage;
using Application.MediatR.Messages.Queries.GetMessageByKey;
using Application.MediatR.Messages.Queries.GetMessages;
using Application.MediatR.Messages.Queries.GetPagedMessages;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1_1;

[ApiVersion("1.1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MessagesController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPagedMessages([FromQuery] GetPagedMessagesQuery query)
    {
        var result = await Mediator.Send(query);
        Response.Headers.Add("X-Pagination", result.ToString());
        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
        => Ok(await Mediator.Send(new GetMessagesQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByKey(int id)
    => Ok(await Mediator.Send(new GetMessageByKeyQuery() { Id = id }));

    [HttpPost]
    public async Task<ActionResult> Create(CreateMessageCommand command)
    => Ok(await Mediator.Send(command));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateMessageCommand command)
    {
        command.Id = id;

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] JsonPatchDocument<UpdateMessageCommand> patchDocument)
    {
        var command = new UpdateMessageCommand();
        patchDocument.ApplyTo(command);
        command.Id = id;

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteMessageCommand(id));

        return NoContent();
    }
}
