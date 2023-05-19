namespace Domain.Events;

public class MessageEmoteAfterUpdateEvent : BaseEvent
{
    public MessageEmoteAfterUpdateEvent(MessageEmote item)
    {
        Item = item;
    }

    public MessageEmote Item { get; }
}
