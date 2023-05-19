using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs.Test;

public class TestHub : Hub<IHubClient>
{
    public Task Test(string message)
    {
        Console.WriteLine($"Test: {message}");
        return Task.CompletedTask;
    }

    public string Test2(string message)
    {
        Console.WriteLine($"Test: {message}");
        return message;
    }

    public Task BroadcastMessage(string message)
    => Clients.All.BroadcastMessage(message);
}
