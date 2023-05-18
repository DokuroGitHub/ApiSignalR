namespace Domain.Events;

public class ConversationAfterUpdateEvent : BaseEvent
{
    public ConversationAfterUpdateEvent(Conversation item)
    {
        Item = item;
    }

    public Conversation Item { get; }
}
