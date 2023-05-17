namespace Application.Common.Models;

#pragma warning disable 
public class LoginResponse
{
    public CurrentUser CurrentUser { get; init; }
    public string Token { get; init; }
}
