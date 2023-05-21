namespace Domain.Entities;

#pragma warning disable
public class ConversationInvitation : BaseEntity
{
    public int Id { get; set; }
    public int ConversationId { get; set; }
    public int UserId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public ConversationRole Role { get; set; }
    public int? JudgedBy { get; set; }
    public DateTime? AcceptedAt { get; set; }
    public DateTime? RejectedAt { get; set; }
    //* ref
    // Conversation
    public virtual Conversation Conversation { get; set; }
    // User
    public virtual User User { get; set; }
    public virtual User Creator { get; set; }
    public virtual User? Judger { get; set; }
}
