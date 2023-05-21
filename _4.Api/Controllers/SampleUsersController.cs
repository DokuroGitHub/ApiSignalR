using Application.MediatR.SampleUsers.Commands.CreateSampleUser;
using Application.MediatR.SampleUsers.Commands.DeleteSampleUser;
using Application.MediatR.SampleUsers.Commands.UpdateSampleUser;
using Application.MediatR.SampleUsers.Queries.GetSampleUserByKey;
using Application.MediatR.SampleUsers.Queries.GetSampleUsers;
using Application.MediatR.SampleUsers.Queries.GetPagedSampleUsers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace Api.Controllers;

// [Authorize]
[ApiVersionNeutral]
public class SampleUsersController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPagedSampleUsers([FromQuery] GetPagedSampleUsersQuery query)
    {
        var result = await Mediator.Send(query);
        Response.Headers.Add("X-Pagination", result.ToString());
        return Ok(result);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await Mediator.Send(new GetSampleUsersQuery()));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetByKey(int id)
    => Ok(await Mediator.Send(new GetSampleUserByKeyQuery() { Id = id }));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Create(CreateSampleUserCommand command)
    => Ok(await Mediator.Send(command));

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, UpdateSampleUserCommand command)
    {
        command.Id = id;

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] JsonPatchDocument<UpdateSampleUserCommand> patchDocument)
    {
        var command = new UpdateSampleUserCommand();
        patchDocument.ApplyTo(command);
        command.Id = id;

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteSampleUserCommand(id));

        return NoContent();
    }
}
