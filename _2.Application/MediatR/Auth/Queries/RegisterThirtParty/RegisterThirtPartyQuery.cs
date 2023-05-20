using System.Text.Json.Serialization;
using Application.Common.Interfaces.AuthThirtParty;
using MediatR;

namespace Application.MediatR.Auth.Queries.RegisterThirtParty;

public record RegisterThirtPartyQuery : IRequest<RegisterResponse>
{
#pragma warning disable
    public string Username { get; init; }
    public string Password { get; init; }
    public string? PasswordConfirm { get; init; }
    public string Email { get; init; }
    public UserGender? Gender { get; init; } = UserGender.Male;
    public string? Name { get; init; }
    public string? Phone { get; init; }
    public DateTime? DateOfBirth { get; init; } = DateTime.Now;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserGender
{
    Male = 0,
    Female = 1
}

public class LoginQueryHandler : IRequestHandler<RegisterThirtPartyQuery, RegisterResponse>
{
    private readonly IAuthThirdPartyService _authThirtPartyService;

    private string _baseUrl { get; set; }
    private HttpClient _client { get; set; }

    public LoginQueryHandler(
        IAuthThirdPartyService authThirtPartyService)
    {
        _authThirtPartyService = authThirtPartyService;
    }

    public Task<RegisterResponse> Handle(RegisterThirtPartyQuery request, CancellationToken cancellationToken)
    => _authThirtPartyService.Register(new RegisterRequest()
    {
        Email = request.Username,
        Password = request.Password,
    });
}
