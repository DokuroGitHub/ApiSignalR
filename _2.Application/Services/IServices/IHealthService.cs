namespace Application.Services.IServices;

public interface IHealthService
{
    Task<bool> IsHealthy { get; }
}