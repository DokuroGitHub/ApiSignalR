using Application.Common.Interfaces;
using MediatR;

namespace Application.MediatR.Auth.Queries.LoginThirtParty;

public record LoginThirtParty : IRequest<LoginResponse>
{
#pragma warning disable
    public string Username { get; init; }
    public string Password { get; init; }
    public string? PasswordConfirm { get; init; }
};

public class LoginQueryHandler : IRequestHandler<LoginThirtParty, LoginResponse>
{
    private readonly IAuthThirtPartyService _authThirtPartyService;

    private string _baseUrl { get; set; }
    private HttpClient _client { get; set; }

    public LoginQueryHandler(
        IAuthThirtPartyService authThirtPartyService)
    {
        _authThirtPartyService = authThirtPartyService;
    }

    public Task<LoginResponse> Handle(LoginThirtParty request, CancellationToken cancellationToken)
    => _authThirtPartyService.Login(new LoginRequest()
    {
    Email = request.Username,
    Password = request.Password,
    });
}
