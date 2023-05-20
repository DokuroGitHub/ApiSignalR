using Application.Services.IServices;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Application.Health;

public class AuthThirdPartyHealthCheck : IHealthCheck
{
    private readonly IAuthThirdPartyHealthService _healthService;

    public AuthThirdPartyHealthCheck(IAuthThirdPartyHealthService healthService) => _healthService = healthService;

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            var isHealthy = _healthService.IsHealthy.Result;
            return isHealthy
                ? HealthCheckResult.Healthy("System is in a healthy state.")
                : HealthCheckResult.Unhealthy("System is in an unhealthy state.");
        });
    }
}
