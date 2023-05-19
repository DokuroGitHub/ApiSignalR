namespace Domain.Events;

public class ConversationBlockBeforeDeleteEvent : BaseEvent
{
    public ConversationBlockBeforeDeleteEvent(ConversationBlock item)
    {
        Item = item;
    }

    public ConversationBlock Item { get; }
}
