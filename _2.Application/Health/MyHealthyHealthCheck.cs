using Application.Services.IServices;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Application.Health;

public class MyHealthyHealthCheck : IHealthCheck
{
    private readonly IMyHealthyHealthService _healthService;

    public MyHealthyHealthCheck(IMyHealthyHealthService healthService) => _healthService = healthService;

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    => Task.Run(() =>
    {
        var isHealthy = _healthService.IsHealthy.Result;
        return isHealthy ?
            Task.FromResult(HealthCheckResult.Healthy("System is in a healthy state.")) :
            Task.FromResult(HealthCheckResult.Unhealthy("System is in an unhealthy state."));
    });
}
