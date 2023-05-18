namespace Domain.Events;

public class ConversationAfterDeleteEvent : BaseEvent
{
    public ConversationAfterDeleteEvent(Conversation item)
    {
        Item = item;
    }

    public Conversation Item { get; }
}
