using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Services.Users;

#pragma warning disable 
public class GetRoleByUserIdDto : IMapFrom<User>
{
    public string Role { get; init; }
}
