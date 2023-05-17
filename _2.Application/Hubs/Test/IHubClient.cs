namespace Application.Hubs.Test;

public interface IHubClient
{
    Task BroadcastMessage(string message);
}