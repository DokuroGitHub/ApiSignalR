using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs.Test;

public class TestHub : Hub<IHubClient>
{
    public Task Test(string message)
    {
        Console.WriteLine($"Test: {message}");
        return Task.CompletedTask;
    }

    public Task BroadcastMessage(string message)
    => Clients.All.BroadcastMessage(message);
}
