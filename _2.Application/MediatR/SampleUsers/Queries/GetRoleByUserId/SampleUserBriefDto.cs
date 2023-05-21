using Application.Common.Mappings;
using Domain.Entities;

namespace Application.MediatR.SampleUsers.Queries.GetRoleBySampleUserId;

#pragma warning disable 
public class SampleUserBriefDto : IMapFrom<SampleUser>
{
    public string Role { get; init; }
}
