namespace Domain.Events;

public class ConversationBeforeUpdateOfLastMessageIdEvent : BaseEvent
{
    public ConversationBeforeUpdateOfLastMessageIdEvent(Conversation item)
    {
        Item = item;
    }

    public Conversation Item { get; }
}
