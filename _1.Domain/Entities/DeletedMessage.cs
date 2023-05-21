namespace Domain.Entities;

#pragma warning disable
public class DeletedMessage : BaseEntity
{
    public int MessageId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    //* ref
    // Message
    public virtual Message Message { get; set; }
    // User
    public virtual User Creator { get; set; }
}
