using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Services.Users;

#pragma warning disable 
public class GetUsernameByUserIdDto : IMapFrom<User>
{
    public string Username { get; init; }
}
