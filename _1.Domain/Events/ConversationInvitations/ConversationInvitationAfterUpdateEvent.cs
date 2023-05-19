namespace Domain.Events;

public class ConversationInvitationAfterUpdateEvent : BaseEvent
{
    public ConversationInvitationAfterUpdateEvent(ConversationInvitation item)
    {
        Item = item;
    }

    public ConversationInvitation Item { get; }
}
