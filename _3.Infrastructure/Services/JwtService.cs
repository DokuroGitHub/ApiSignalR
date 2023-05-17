using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Common;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly IDateTimeService _dateTimeService;
    private readonly Appsettings _appSettings;

    public JwtService(
        IDateTimeService dateTimeService,
        Appsettings appSettings)
    {
        _dateTimeService = dateTimeService;
        _appSettings = appSettings;
    }

    public string GenerateJWT(CurrentUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim("Id", user.UserId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim("Email", user.Email??""),
            new Claim("Role", user.Role),
            new Claim(ClaimTypes.Role, user.Role),
        };
        var token = new JwtSecurityToken(
            claims: claims,
            expires: _dateTimeService.Now.AddDays(_appSettings.Jwt.ExpireDays),
            audience: _appSettings.Jwt.Audience,
            issuer: _appSettings.Jwt.Issuer,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal Validate(string token)
    {
        IdentityModelEventSource.ShowPII = true;
        TokenValidationParameters validationParameters = new()
        {
            ValidateLifetime = true,
            ValidAudience = _appSettings.Jwt.Audience,
            ValidIssuer = _appSettings.Jwt.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.Key))
        };

        var principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);

        return principal;
    }
}
