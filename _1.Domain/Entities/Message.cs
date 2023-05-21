namespace Domain.Entities;

#pragma warning disable
public class Message : BaseEntity
{
    public int Id { get; set; }
    public int ConversationId { get; set; }
    public int CreatedBy { get; set; }
    public int? DeletedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? Content { get; set; }
    public int? ReplyTo { get; set; }
    //* ref
    // Conversation
    public virtual Conversation Conversation { get; set; }
    public virtual Conversation? LastMessageNoConversation { get; set; }
    // User
    public virtual User Creator { get; set; }
    public virtual User? Deleter { get; set; }
    // Message
    public virtual Message? ReplyToMessage { get; set; }
    public virtual ICollection<Message> Replies { get; set; }
    // DeletedMessage
    public virtual ICollection<DeletedMessage> DeletedMessages { get; set; }
    // MessageAttachment
    public virtual ICollection<MessageAttachment> Attachments { get; set; }
    // MessageEmote
    public virtual ICollection<MessageEmote> Emotes { get; set; }
}
