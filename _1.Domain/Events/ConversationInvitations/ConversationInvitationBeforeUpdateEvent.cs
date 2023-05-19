namespace Domain.Events;

public class ConversationInvitationBeforeUpdateEvent : BaseEvent
{
    public ConversationInvitationBeforeUpdateEvent(ConversationInvitation item)
    {
        Item = item;
    }

    public ConversationInvitation Item { get; }
}
