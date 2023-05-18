namespace Domain.Entities;

#pragma warning disable
public class Message : BaseEntity
{
    public int Id { get; set; }
    public int ConversationId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? Content { get; set; }
    public int? ReplyTo { get; set; }
    // ref
    public virtual Conversation Conversation { get; set; }
    public virtual Conversation? LastMessageOfConversation { get; set; }
    public virtual Message? ReplyToMessage { get; set; }
    public virtual ICollection<Message> Replies { get; set; }
    public virtual ICollection<DeletedMessage> DeletedMessages { get; set; }
    public virtual ICollection<MessageAttachment> Attachments { get; set; }
    public virtual ICollection<MessageEmote> Emotes { get; set; }
}
