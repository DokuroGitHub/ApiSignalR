using Application.Common.Mappings;
using Domain.Entities;
using Domain.Enums;

namespace Application.MediatR.Participants.Queries.GetParticipantByKey;

#pragma warning disable 
public class ParticipantBriefDto : IMapFrom<Participant>
{
    public int ConversationId { get; init; }
    public int UserId { get; init; }
    public string? Nickname { get; init; }
    public int CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public int? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public int? DeletedBy { get; init; }
    public DateTime? DeletedAt { get; init; }
    public ConversationRole Role { get; init; }
}
