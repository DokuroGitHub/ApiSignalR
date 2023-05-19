namespace Domain.Events;

public class MessageEmoteBeforeInsertEvent : BaseEvent
{
    public MessageEmoteBeforeInsertEvent(MessageEmote item)
    {
        Item = item;
    }

    public MessageEmote Item { get; }
}
