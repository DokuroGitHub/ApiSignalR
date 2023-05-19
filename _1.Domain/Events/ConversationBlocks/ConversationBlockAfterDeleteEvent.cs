namespace Domain.Events;

public class ConversationBlockAfterDeleteEvent : BaseEvent
{
    public ConversationBlockAfterDeleteEvent(ConversationBlock item)
    {
        Item = item;
    }

    public ConversationBlock Item { get; }
}
