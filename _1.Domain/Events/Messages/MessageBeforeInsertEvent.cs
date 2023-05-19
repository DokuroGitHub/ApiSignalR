namespace Domain.Events;

public class MessageBeforeInsertEvent : BaseEvent
{
    public MessageBeforeInsertEvent(Message item)
    {
        Item = item;
    }

    public Message Item { get; }
}
