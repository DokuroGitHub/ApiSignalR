namespace Domain.Entities;

#pragma warning disable
public class MessageAttachment : BaseEntity
{
    public int Id { get; set; }
    public int MessageId { get; set; }
    public string? FileUrl { get; set; }
    public string? ThumbUrl { get; set; }
    public AttachmentType Type { get; set; }
    // ref
    public virtual Message Message { get; set; }
}
