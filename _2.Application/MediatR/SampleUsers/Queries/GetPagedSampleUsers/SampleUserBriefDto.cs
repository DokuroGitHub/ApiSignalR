using Application.Common.Mappings;
using Domain.Entities;

namespace Application.MediatR.SampleUsers.Queries.GetPagedSampleUsers;

#pragma warning disable 
public class SampleUserBriefDto : IMapFrom<SampleUser>
{
    public int Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string DisplayName { get; init; }
    public string? Email { get; init; }
}
