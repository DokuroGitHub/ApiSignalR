using Application.Common.Mappings;
using Domain.Entities;

namespace Application.MediatR.Users.Queries.GetUsernameByUserId;

#pragma warning disable 
public class UserBriefDto : IMapFrom<User>
{
    public string Username { get; init; }
}
