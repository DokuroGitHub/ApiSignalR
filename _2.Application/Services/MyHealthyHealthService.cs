using Application.Services.IServices;

namespace Application.Services;

public class MyHealthyHealthService : IMyHealthyHealthService
{
    public Task<bool> IsHealthy { get; private set; } = Task.Run(() => true);
}
