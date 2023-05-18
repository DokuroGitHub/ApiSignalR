namespace Domain.Entities;

#pragma warning disable
public class DeletedMessage : BaseEntity
{
    public int MessageId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    // ref
    public virtual Message Message { get; set; }
}
