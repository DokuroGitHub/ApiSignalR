using Application.Common.Mappings;
using Domain.Entities;
using Domain.Enums;

namespace Application.MediatR.MessageEmotes.Queries.GetPagedMessageEmotes;

#pragma warning disable 
public class MessageEmoteBriefDto : IMapFrom<MessageEmote>
{
    public int MessageId { get; init; }
    public int UserId { get; init; }
    public DateTime UpdatedAt { get; init; }
    public EmoteCode Code { get; init; }
}
