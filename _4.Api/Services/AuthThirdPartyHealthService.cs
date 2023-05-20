using Application.Services.IServices;

namespace Api.Services;

public class AuthThirdPartyHealthService : IAuthThirdPartyHealthService
{
    private string _baseUrl { get; set; }
    private HttpClient _client { get; set; }

    public AuthThirdPartyHealthService(
        IHttpClientFactory httpClientFactory
    )
    {
        _client = httpClientFactory.CreateClient();
        _baseUrl = "https://trainingmanagementsystem.azurewebsites.net/hc";
    }

    public Task<bool> IsHealthy
    {
        get => Task.Run(async () =>
        {
            var response = await _client.GetAsync($"{_baseUrl}");
            return response.IsSuccessStatusCode;
        });
    }
}
