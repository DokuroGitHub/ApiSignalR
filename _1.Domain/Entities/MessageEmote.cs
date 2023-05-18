namespace Domain.Entities;

#pragma warning disable
public class MessageEmote : BaseEntity
{
    public int Id { get; set; }
    public int MessageId { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public EmoteCode Code { get; set; }
    // ref
    public virtual Message Message { get; set; }
}
