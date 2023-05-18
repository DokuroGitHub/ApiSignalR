namespace Domain.Events;

public class ConversationBeforeUpdateEvent : BaseEvent
{
    public ConversationBeforeUpdateEvent(Conversation item)
    {
        Item = item;
    }

    public Conversation Item { get; }
}
