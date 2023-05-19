using Application.Common.Models;

namespace Application.MediatR.Auth.Queries.Login;

#pragma warning disable 
public class LoginResponse
{
    public CurrentUser CurrentUser { get; init; }
    public string Token { get; init; }
}
