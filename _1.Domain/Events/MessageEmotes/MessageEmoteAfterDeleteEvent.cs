namespace Domain.Events;

public class MessageEmoteAfterDeleteEvent : BaseEvent
{
    public MessageEmoteAfterDeleteEvent(MessageEmote item)
    {
        Item = item;
    }

    public MessageEmote Item { get; }
}
