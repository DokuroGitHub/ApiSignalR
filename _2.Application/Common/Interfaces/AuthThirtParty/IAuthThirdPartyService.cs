namespace Application.Common.Interfaces.AuthThirdParty;

public interface IAuthThirdPartyService
{
    Task<LoginResponse> Login(LoginRequest dto);
    Task<RegisterResponse> Register(RegisterRequest dto);
}
