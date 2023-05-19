namespace Domain.Events;

public class MessageAttachmentAfterUpdateEvent : BaseEvent
{
    public MessageAttachmentAfterUpdateEvent(MessageAttachment item)
    {
        Item = item;
    }

    public MessageAttachment Item { get; }
}
