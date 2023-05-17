using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Hubs.Users;

#pragma warning disable 
public class UserBriefDto : IMapFrom<User>
{
    public int Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string DisplayName { get; init; }
    public string? Email { get; init; }
}
