namespace Domain.Events;

public class ConversationInvitationAfterDeleteEvent : BaseEvent
{
    public ConversationInvitationAfterDeleteEvent(ConversationInvitation item)
    {
        Item = item;
    }

    public ConversationInvitation Item { get; }
}
