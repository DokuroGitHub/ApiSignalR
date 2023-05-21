namespace Domain.Entities;

#pragma warning disable
public class MessageEmote : BaseEntity
{
    public int MessageId { get; set; }
    public int UserId { get; set; }
    public DateTime UpdatedAt { get; set; }
    public EmoteCode Code { get; set; }
    //* ref
    // Message
    public virtual Message Message { get; set; }
    // User
    public virtual User User { get; set; }
}
