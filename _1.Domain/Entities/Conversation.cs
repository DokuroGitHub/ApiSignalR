namespace Domain.Entities;

#pragma warning disable
public class Conversation : BaseEntity
{
    public int Id { get; set; }
    public int CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public int? DeletedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PhotoUrl { get; set; }
    public int? LastMessageId { get; set; }
    //* ref
    // User
    public virtual User Creator { get; set; }
    public virtual User? Updater { get; set; }
    public virtual User? Deleter { get; set; }
    // Message
    public virtual Message? LastMessage { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
    // ConversationInvitation
    public virtual ICollection<ConversationInvitation> Invitations { get; set; }
    // ConversationBlock
    public virtual ICollection<ConversationBlock> Blocks { get; set; }
    // Participant
    public virtual ICollection<Participant> Participants { get; set; }
}
