namespace Application.MediatR.Auth.Queries.LoginThirtParty;

#pragma warning disable
public class LoginRequest
{
    public string Email { get; init; }
    public string Password { get; init; }
}
