namespace Domain.Events;

public class ConversationAfterInsertEvent : BaseEvent
{
    public ConversationAfterInsertEvent(Conversation item)
    {
        Item = item;
    }

    public Conversation Item { get; }
}
