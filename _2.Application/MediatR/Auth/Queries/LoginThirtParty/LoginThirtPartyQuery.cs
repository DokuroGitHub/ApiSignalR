using Application.Common.Interfaces.AuthThirtParty;
using MediatR;

namespace Application.MediatR.Auth.Queries.LoginThirtParty;

public record LoginThirtPartyQuery : IRequest<LoginResponse>
{
#pragma warning disable
    public string Email { get; init; }
    public string Password { get; init; }
    public string? PasswordConfirm { get; init; }
};

public class LoginQueryHandler : IRequestHandler<LoginThirtPartyQuery, LoginResponse>
{
    private readonly IAuthThirtPartyService _authThirtPartyService;

    public LoginQueryHandler(
        IAuthThirtPartyService authThirtPartyService)
    {
        _authThirtPartyService = authThirtPartyService;
    }

    public Task<LoginResponse> Handle(LoginThirtPartyQuery request, CancellationToken cancellationToken)
    => _authThirtPartyService.Login(new LoginRequest()
    {
        Email = request.Email,
        Password = request.Password,
    });
}
