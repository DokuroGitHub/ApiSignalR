namespace Domain.Events;

public class MessageAttachmentAfterInsertEvent : BaseEvent
{
    public MessageAttachmentAfterInsertEvent(MessageAttachment item)
    {
        Item = item;
    }

    public MessageAttachment Item { get; }
}
