using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestSharp;

namespace Api.Health;

public class ApiHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var url = "https://9gag.com";
        var client = new RestClient();
        var request = new RestRequest(url, Method.Get);
        var response = client.Execute(request);
        if (response.IsSuccessful)
            return Task.FromResult(HealthCheckResult.Healthy());
        else
            return Task.FromResult(HealthCheckResult.Unhealthy());
    }
}
