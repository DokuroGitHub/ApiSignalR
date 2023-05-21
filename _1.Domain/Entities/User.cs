namespace Domain.Entities;

#pragma warning disable
public class User : BaseEntity
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? Token { get; set; }
    public string? AvatarUrl { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    // ghost
    public string DisplayName { get; set; }
    //* ref
    // User
    public virtual User? Creator { get; set; }
    public virtual User? Updater { get; set; }
    public virtual ICollection<User> CreatedUsers { get; set; }
    public virtual ICollection<User> UpdatedUsers { get; set; }
    // Conversation
    public virtual ICollection<Conversation> CreatedConversations { get; set; }
    public virtual ICollection<Conversation> UpdatedConversations { get; set; }
    public virtual ICollection<Conversation> DeletedConversations { get; set; }
    // ConversationBlock
    public virtual ICollection<ConversationBlock> ConversationBlocks { get; set; }
    public virtual ICollection<ConversationBlock> CreatedConversationBlocks { get; set; }
    // ConversationInvitation
    public virtual ICollection<ConversationInvitation> Invitations { get; set; }
    public virtual ICollection<ConversationInvitation> CreatedInvitations { get; set; }
    public virtual ICollection<ConversationInvitation> JudgedInvitations { get; set; }
    // DeletedMessage
    public virtual ICollection<DeletedMessage> CreatedDeletedMessages { get; set; }
    // Message
    public virtual ICollection<Message> CreatedMessages { get; set; }
    public virtual ICollection<Message> DeletedMessages { get; set; }
    // MessageEmote
    public virtual ICollection<MessageEmote> MessageEmotes { get; set; }
    // Participant
    public virtual ICollection<Participant> Participants { get; set; }
    public virtual ICollection<Participant> CreatedParticipants { get; set; }
    public virtual ICollection<Participant> UpdatedParticipants { get; set; }
    public virtual ICollection<Participant> DeletedParticipants { get; set; }
}
