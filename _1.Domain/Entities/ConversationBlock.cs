namespace Domain.Entities;

#pragma warning disable
public class ConversationBlock : BaseEntity
{
    public int ConversationId { get; set; }
    public int UserId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    //* ref
    // Conversation
    public virtual Conversation Conversation { get; set; }
    // User
    public virtual User User { get; set; }
    public virtual User Creator { get; set; }
}
