namespace Domain.Events;

public class ConversationBlockAfterUpdateEvent : BaseEvent
{
    public ConversationBlockAfterUpdateEvent(ConversationBlock item)
    {
        Item = item;
    }

    public ConversationBlock Item { get; }
}
