namespace Application.Common.Models;

public class CurrentUser
{
#pragma warning disable
    public int UserId { get; init; }
    public string DisplayName { get; init; }
    public string? Email { get; init; }
    public string Role { get; init; }
}
