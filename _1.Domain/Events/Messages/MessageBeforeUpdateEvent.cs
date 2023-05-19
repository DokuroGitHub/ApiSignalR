namespace Domain.Events;

public class MessageBeforeUpdateEvent : BaseEvent
{
    public MessageBeforeUpdateEvent(Message item)
    {
        Item = item;
    }

    public Message Item { get; }
}
