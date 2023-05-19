namespace Domain.Events;

public class ConversationAfterUpdateOfLastMessageIdEvent : BaseEvent
{
    public ConversationAfterUpdateOfLastMessageIdEvent(Conversation item)
    {
        Item = item;
    }

    public Conversation Item { get; }
}
