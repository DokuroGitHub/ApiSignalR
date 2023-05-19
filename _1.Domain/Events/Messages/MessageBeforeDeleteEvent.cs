namespace Domain.Events;

public class MessageBeforeDeleteEvent : BaseEvent
{
    public MessageBeforeDeleteEvent(Message item)
    {
        Item = item;
    }

    public Message Item { get; }
}
