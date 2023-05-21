using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Services.SampleUsers;

#pragma warning disable 
public class GetUsernameByUserIdDto : IMapFrom<SampleUser>
{
    public string Username { get; init; }
}
