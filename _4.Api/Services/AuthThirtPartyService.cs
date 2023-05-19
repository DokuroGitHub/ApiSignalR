using System.Text;
using Application.Common.Interfaces.AuthThirtParty;
using Newtonsoft.Json;

namespace Api.Services;

public class AuthThirtPartyService : IAuthThirtPartyService
{
    private string _baseUrl { get; set; }
    private HttpClient _client { get; set; }

    public AuthThirtPartyService(
        IHttpClientFactory httpClientFactory
    )
    {
        _client = httpClientFactory.CreateClient();
        _baseUrl = "http://www.srstrainingmanagementsystem.somee.com/api/Account";
    }

    public async Task<LoginResponse> Login(LoginRequest dto)
    {
        var stringContent = new StringContent(
            JsonConvert.SerializeObject(dto, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }),
            Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"{_baseUrl}/login", stringContent);
        if (!response.IsSuccessStatusCode)
            throw new UnauthorizedAccessException("Login failed");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<LoginResponse>(content);
        if (result == null)
            throw new UnauthorizedAccessException("Login failed");
        return result;
    }

    public async Task<RegisterResponse> Register(RegisterRequest dto)
    {
        var stringContent = new StringContent(
            JsonConvert.SerializeObject(dto, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }),
            Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"{_baseUrl}/register", stringContent);
        if (!response.IsSuccessStatusCode)
            throw new UnauthorizedAccessException("Register failed");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<RegisterResponse>(content);
        if (result == null)
            throw new UnauthorizedAccessException("Register failed");
        return result;
    }
}
