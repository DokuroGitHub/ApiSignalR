namespace Domain.Events;

public class ConversationBeforeDeleteEvent : BaseEvent
{
    public ConversationBeforeDeleteEvent(Conversation item)
    {
        Item = item;
    }

    public Conversation Item { get; }
}
