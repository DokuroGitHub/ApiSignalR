namespace Domain.Events;

public class MessageAfterUpdateEvent : BaseEvent
{
    public MessageAfterUpdateEvent(Message item)
    {
        Item = item;
    }

    public Message Item { get; }
}
