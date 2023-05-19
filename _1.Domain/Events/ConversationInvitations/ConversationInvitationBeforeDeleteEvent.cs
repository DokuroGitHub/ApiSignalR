namespace Domain.Events;

public class ConversationInvitationBeforeDeleteEvent : BaseEvent
{
    public ConversationInvitationBeforeDeleteEvent(ConversationInvitation item)
    {
        Item = item;
    }

    public ConversationInvitation Item { get; }
}
