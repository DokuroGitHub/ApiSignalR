using Application.MediatR.Auth.Queries.LoginAdmin;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1_1;

[ApiVersion("1.1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ApiControllerBase
{
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> LoginAdmin(LoginAdminQuery query)
    => Ok(await Mediator.Send(query));
}
