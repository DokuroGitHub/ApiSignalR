using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Services.SampleUsers;

#pragma warning disable 
public class GetRoleByUserIdDto : IMapFrom<SampleUser>
{
    public string Role { get; init; }
}
