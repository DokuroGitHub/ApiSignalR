namespace Domain.Events;

public class MessageAfterInsertEvent : BaseEvent
{
    public MessageAfterInsertEvent(Message item)
    {
        Item = item;
    }

    public Message Item { get; }
}
