using Application.Common.Mappings;
using Domain.Entities;

namespace Application.MediatR.SampleUsers.Queries.GetSampleUsernameBySampleUserId;

#pragma warning disable 
public class SampleUserBriefDto : IMapFrom<SampleUser>
{
    public string SampleUsername { get; init; }
}
