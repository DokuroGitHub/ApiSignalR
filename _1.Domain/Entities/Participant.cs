namespace Domain.Entities;

#pragma warning disable
public class Participant : BaseEntity
{
    public int ConversationId { get; set; }
    public int UserId { get; set; }
    public string? Nickname { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public ConversationRole Role { get; set; }
    // ref
    public virtual Conversation Conversation { get; set; }
}
