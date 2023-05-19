namespace Domain.Events;

public class MessageAfterDeleteEvent : BaseEvent
{
    public MessageAfterDeleteEvent(Message item)
    {
        Item = item;
    }

    public Message Item { get; }
}
