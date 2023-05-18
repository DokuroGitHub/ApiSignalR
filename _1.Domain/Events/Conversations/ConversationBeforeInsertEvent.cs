namespace Domain.Events;

public class ConversationBeforeInsertEvent : BaseEvent
{
    public ConversationBeforeInsertEvent(Conversation item)
    {
        Item = item;
    }

    public Conversation Item { get; }
}
