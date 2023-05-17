using Application.Common.Mappings;
using Domain.Entities;

namespace Application.MediatR.Users.Queries.GetRoleByUserId;

#pragma warning disable 
public class UserBriefDto : IMapFrom<User>
{
    public string Role { get; init; }
}
