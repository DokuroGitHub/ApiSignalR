using Application.Common.Mappings;
using Domain.Entities;

namespace Application.MediatR.MessageAttachments.Queries.GetMessageAttachments;

#pragma warning disable 
public class MessageAttachmentBriefDto : IMapFrom<MessageAttachment>
{
    public int Id { get; init; }
    public int CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public int? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public int? DeletedBy { get; init; }
    public DateTime? DeletedAt { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public string? PhotoUrl { get; init; }
    public int? LastMessageId { get; init; }
}
