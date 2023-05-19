namespace Domain.Events;

public class MessageAttachmentBeforeUpdateEvent : BaseEvent
{
    public MessageAttachmentBeforeUpdateEvent(MessageAttachment item)
    {
        Item = item;
    }

    public MessageAttachment Item { get; }
}
