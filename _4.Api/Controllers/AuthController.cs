using Application.MediatR.Auth.Commands.Register;
using Application.MediatR.Auth.Queries.Login;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiVersionNeutral]
public class AuthController : ApiControllerBase
{
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Login(LoginQuery query)
    => Ok(await Mediator.Send(query));

    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Register(RegisterCommand command)
    => Ok(await Mediator.Send(command));
}
