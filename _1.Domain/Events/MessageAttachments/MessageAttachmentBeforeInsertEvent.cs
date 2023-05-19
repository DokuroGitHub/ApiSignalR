namespace Domain.Events;

public class MessageAttachmentBeforeInsertEvent : BaseEvent
{
    public MessageAttachmentBeforeInsertEvent(MessageAttachment item)
    {
        Item = item;
    }

    public MessageAttachment Item { get; }
}
