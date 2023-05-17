using Microsoft.AspNetCore.Mvc;
using Application.MediatR.App.Queries.GetAppsettings;

namespace Api.Controllers;

// [Authorize]
[ApiVersionNeutral]
public class AppController : ApiControllerBase
{
    [HttpGet("appsettings")]
    public async Task<IActionResult> GetAppsettings()
        => Ok(await Mediator.Send(new GetAppsettingsQuery()));
}
