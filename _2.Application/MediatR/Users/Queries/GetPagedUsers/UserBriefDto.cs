using Application.Common.Mappings;
using Domain.Entities;

namespace Application.MediatR.Users.Queries.GetPagedUsers;

#pragma warning disable 
public class UserBriefDto : IMapFrom<User>
{
    public int Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string DisplayName { get; init; }
    public string? Email { get; init; }
}
