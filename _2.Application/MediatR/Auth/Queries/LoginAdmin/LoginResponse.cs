using Application.Common.Models;

namespace Application.MediatR.Auth.Queries.LoginAdmin;

#pragma warning disable 
public class LoginResponse
{
    public CurrentUser CurrentUser { get; init; }
    public string Token { get; init; }
}
