namespace Application.Common.Interfaces.AuthThirtParty;

public interface IAuthThirtPartyService
{
    Task<LoginResponse> Login(LoginRequest dto);
    Task<RegisterResponse> Register(RegisterRequest dto);
}
