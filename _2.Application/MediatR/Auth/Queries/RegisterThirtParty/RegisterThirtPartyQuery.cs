using Application.Common.Interfaces;
using MediatR;

namespace Application.MediatR.Auth.Queries.RegisterThirtParty;

public record RegisterThirtPartyQuery : IRequest<RegisterResponse>
{
#pragma warning disable
    public string Username { get; init; }
    public string Password { get; init; }
    public string? PasswordConfirm { get; init; }
};

public class LoginQueryHandler : IRequestHandler<RegisterThirtPartyQuery, RegisterResponse>
{
    private readonly IAuthThirtPartyService _authThirtPartyService;

    private string _baseUrl { get; set; }
    private HttpClient _client { get; set; }

    public LoginQueryHandler(
        IAuthThirtPartyService authThirtPartyService)
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
