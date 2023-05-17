using System.Security.Claims;
using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateJWT(CurrentUser user);
    ClaimsPrincipal Validate(string token);
}
