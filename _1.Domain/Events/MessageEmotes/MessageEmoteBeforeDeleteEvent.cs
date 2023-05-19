namespace Domain.Events;

public class MessageEmoteBeforeDeleteEvent : BaseEvent
{
    public MessageEmoteBeforeDeleteEvent(MessageEmote item)
    {
        Item = item;
    }

    public MessageEmote Item { get; }
}
