namespace Domain.Events;

public class MessageAttachmentBeforeDeleteEvent : BaseEvent
{
    public MessageAttachmentBeforeDeleteEvent(MessageAttachment item)
    {
        Item = item;
    }

    public MessageAttachment Item { get; }
}
