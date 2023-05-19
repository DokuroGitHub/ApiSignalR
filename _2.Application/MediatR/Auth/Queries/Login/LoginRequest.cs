namespace Application.MediatR.Auth.Queries.Login;

#pragma warning disable
public class LoginRequest
{
    public string Username { get; init; }
    public string Password { get; init; }
    public string? PasswordConfirm { get; init; }
}
