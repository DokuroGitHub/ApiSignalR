using Microsoft.AspNetCore.Mvc;
using Domain.Common;

namespace Api.Controllers;

// [Authorize(Policy = PolicyNames.AdminOnly)]
[ApiVersionNeutral]
public class AppController : ApiControllerBase
{
    private readonly Appsettings _appsettings;

    public AppController(Appsettings appsettings)
    {
        _appsettings = appsettings;
    }

    [HttpGet("appsettings")]
    public IActionResult GetAppsettings()
        => Ok(_appsettings);
}
