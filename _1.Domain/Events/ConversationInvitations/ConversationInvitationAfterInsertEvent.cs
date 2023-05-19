namespace Domain.Events;

public class ConversationInvitationAfterInsertEvent : BaseEvent
{
    public ConversationInvitationAfterInsertEvent(ConversationInvitation item)
    {
        Item = item;
    }

    public ConversationInvitation Item { get; }
}
