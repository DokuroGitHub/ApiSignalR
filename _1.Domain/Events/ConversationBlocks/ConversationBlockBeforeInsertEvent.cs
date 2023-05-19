namespace Domain.Events;

public class ConversationBlockBeforeInsertEvent : BaseEvent
{
    public ConversationBlockBeforeInsertEvent(ConversationBlock item)
    {
        Item = item;
    }

    public ConversationBlock Item { get; }
}
