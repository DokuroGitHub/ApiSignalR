using System.Security.Claims;

using Application.Common.Interfaces;

namespace Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId
    {
        get
        {
            var authorization = _httpContextAccessor.HttpContext?.Request.Headers.Authorization;
            var idString = _httpContextAccessor.HttpContext?.User?.FindFirstValue("ID");
            var nameIdentifierString = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine($"authorization: {authorization}");
            Console.WriteLine($"idString: {idString}");
            Console.WriteLine($"nameIdentifierString: {nameIdentifierString}");
            return int.TryParse(idString, out int id) ?
                id : int.TryParse(nameIdentifierString, out int nameIdentifier) ?
                nameIdentifier : null;
        }
    }

}
