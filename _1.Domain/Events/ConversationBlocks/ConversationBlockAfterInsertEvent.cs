namespace Domain.Events;

public class ConversationBlockAfterInsertEvent : BaseEvent
{
    public ConversationBlockAfterInsertEvent(ConversationBlock item)
    {
        Item = item;
    }

    public ConversationBlock Item { get; }
}
