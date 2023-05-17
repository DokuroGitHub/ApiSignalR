using Application.Common.Mappings;
using Domain.Entities;

namespace Application.MediatR.Auth.Queries.Login;

#pragma warning disable 
public class UserBriefDto : IMapFrom<User>
{
    public int Id { get; init; }
    public string DisplayName { get; init; }
    public string Email { get; init; }
    public string Role { get; init; }
    public string PasswordHash { get; init; }
}
