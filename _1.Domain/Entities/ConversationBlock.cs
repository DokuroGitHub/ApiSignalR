namespace Domain.Entities;

#pragma warning disable
public class ConversationBlock : BaseEntity
{
    public int ConversationId { get; set; }
    public int UserId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    // ref
    public virtual Conversation Conversation { get; set; }
}
