using Application.Common.Interfaces;
using Application.MediatR.Auth.Commands.LoginAnonymous;
using Application.MediatR.Auth.Commands.Register;
using Application.MediatR.Auth.Queries.Login;
using Application.MediatR.Auth.Queries.LoginAdmin;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiVersionNeutral]
public class AuthController : ApiControllerBase
{
    private readonly ICurrentUserService _currentUserService;

    public AuthController(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> Login(LoginQuery query)
    => Ok(await Mediator.Send(query));

    [HttpPost("[action]")]
    public async Task<ActionResult> LoginAnonymous(LoginAnonymousCommand command)
    => Ok(await Mediator.Send(command));

    [HttpPost("[action]")]
    public async Task<ActionResult> Register(RegisterCommand command)
    => Ok(await Mediator.Send(command));

    [HttpPost("[action]")]
    public async Task<ActionResult> LoginAdmin(LoginAdminQuery query)
    => Ok(await Mediator.Send(query));

    [HttpGet("[action]")]
    public ActionResult GetCurrentUserId()
    => Ok(_currentUserService.UserId);
}
