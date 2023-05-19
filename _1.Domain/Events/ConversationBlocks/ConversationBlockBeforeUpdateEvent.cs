namespace Domain.Events;

public class ConversationBlockBeforeUpdateEvent : BaseEvent
{
    public ConversationBlockBeforeUpdateEvent(ConversationBlock item)
    {
        Item = item;
    }

    public ConversationBlock Item { get; }
}
