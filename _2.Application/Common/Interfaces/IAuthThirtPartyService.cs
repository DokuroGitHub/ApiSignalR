using Application.MediatR.Auth.Queries.LoginThirtParty;
using Application.MediatR.Auth.Queries.RegisterThirtParty;

namespace Application.Common.Interfaces;

public interface IAuthThirtPartyService
{
    Task<LoginResponse?> Login(LoginRequest dto);
    Task<RegisterResponse?> Register(RegisterRequest dto);
}
