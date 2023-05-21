using System.Text.Json.Serialization;

namespace Application.Common.Interfaces.AuthThirdParty;

#pragma warning disable
public class RegisterRequest
{
    public string Email { get; init; }
    public UserGender? Gender { get; init; } = UserGender.Male;
    public string? Name { get; init; }
    public string? Phone { get; init; }
    public string Password { get; init; }
    public DateTime? DateOfBirth { get; init; } = DateTime.Now;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserGender
{
    Male = 0,
    Female = 1
}
