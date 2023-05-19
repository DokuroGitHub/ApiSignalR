namespace Domain.Events;

public class ConversationInvitationBeforeInsertEvent : BaseEvent
{
    public ConversationInvitationBeforeInsertEvent(ConversationInvitation item)
    {
        Item = item;
    }

    public ConversationInvitation Item { get; }
}
