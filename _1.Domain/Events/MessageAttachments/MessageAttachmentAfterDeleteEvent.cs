namespace Domain.Events;

public class MessageAttachmentAfterDeleteEvent : BaseEvent
{
    public MessageAttachmentAfterDeleteEvent(MessageAttachment item)
    {
        Item = item;
    }

    public MessageAttachment Item { get; }
}
