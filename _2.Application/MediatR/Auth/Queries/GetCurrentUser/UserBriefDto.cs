using Application.Common.Mappings;
using Domain.Entities;
using Domain.Enums;

namespace Application.MediatR.Auth.Queries.GetCurrentUser;

#pragma warning disable 
public class UserBriefDto : IMapFrom<User>
{
    public int Id { get; init; }
    public string? UserId { get; init; }
    public string? Token { get; init; }
    public string? AvatarUrl { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string Username { get; init; }
    public UserRole Role { get; init; }
    public int? CreatedBy { get; init; }
    public int? UpdatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    // ghost
    public string DisplayName { get; init; }
}
