namespace Application.Common.Interfaces.AuthThirtParty;

public interface IAuthThirdPartyService
{
    Task<LoginResponse> Login(LoginRequest dto);
    Task<RegisterResponse> Register(RegisterRequest dto);
}
