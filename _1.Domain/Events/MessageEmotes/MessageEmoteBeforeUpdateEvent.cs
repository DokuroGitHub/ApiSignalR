namespace Domain.Events;

public class MessageEmoteBeforeUpdateEvent : BaseEvent
{
    public MessageEmoteBeforeUpdateEvent(MessageEmote item)
    {
        Item = item;
    }

    public MessageEmote Item { get; }
}
