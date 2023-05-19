using Application.MediatR.Auth.Queries.LoginThirtParty;
using Application.MediatR.Auth.Queries.RegisterThirtParty;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1_1;

[ApiVersion("1.1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ApiControllerBase
{
    [HttpPost("[action]")]
    public async Task<ActionResult> LoginThirtParty(LoginThirtPartyQuery query)
    => Ok(await Mediator.Send(query));

    [HttpPost("[action]")]
    public async Task<ActionResult> RegisterThirtParty(RegisterThirtPartyQuery query)
    => Ok(await Mediator.Send(query));
}
