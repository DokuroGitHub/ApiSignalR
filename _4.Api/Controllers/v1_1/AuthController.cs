using Application.MediatR.Auth.Commands.LoginThirdParty;
using Application.MediatR.Auth.Queries.RegisterThirdParty;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1_1;

[ApiVersion("1.1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ApiControllerBase
{
    [HttpPost("[action]")]
    public async Task<ActionResult> LoginThirdParty(LoginThirdPartyCommand command)
    => Ok(await Mediator.Send(command));

    [HttpPost("[action]")]
    public async Task<ActionResult> RegisterThirdParty(RegisterThirdPartyQuery query)
    => Ok(await Mediator.Send(query));
}
