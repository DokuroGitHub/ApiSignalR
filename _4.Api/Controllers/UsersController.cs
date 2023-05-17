using Application.Common.Models;
using Application.MediatR.Users.Commands.CreateUser;
using Application.MediatR.Users.Commands.DeleteUser;
using Application.MediatR.Users.Commands.UpdateUser;
using Application.MediatR.Users.Queries.GetUserByKey;
using Application.MediatR.Users.Queries.GetUsers;
using Application.MediatR.Users.Queries.GetPagedUsers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace Api.Controllers;

// [Authorize]
[ApiVersionNeutral]
public class UsersController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPagedUsers([FromQuery] GetPagedUsersQuery query)
    {
        var result = await Mediator.Send(query);
        Response.Headers.Add("X-Pagination", result.ToString());
        return Ok(result);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
        => Ok(await Mediator.Send(new GetUsersQuery()));

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetByKey(int id)
    => Ok(await Mediator.Send(new GetUserByKeyQuery() { Id = id }));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Create(CreateUserCommand command)
    => Ok(await Mediator.Send(command));

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, UpdateUserCommand command)
    {
        command.Id = id;

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] JsonPatchDocument<UpdateUserCommand> patchDocument)
    {
        var command = new UpdateUserCommand();
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
        await Mediator.Send(new DeleteUserCommand(id));

        return NoContent();
    }
}
