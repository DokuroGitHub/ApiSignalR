namespace Domain.Events;

public class MessageEmoteAfterInsertEvent : BaseEvent
{
    public MessageEmoteAfterInsertEvent(MessageEmote item)
    {
        Item = item;
    }

    public MessageEmote Item { get; }
}
