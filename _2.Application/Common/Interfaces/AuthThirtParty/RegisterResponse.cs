namespace Application.Common.Interfaces.AuthThirdParty;

#pragma warning disable
public class RegisterResponse
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public DateTime DateOfBirth { get; init; }
    public string Role { get; init; }
    public string Token { get; init; }
    public DateTime? ExpireDay { get; init; }
}
