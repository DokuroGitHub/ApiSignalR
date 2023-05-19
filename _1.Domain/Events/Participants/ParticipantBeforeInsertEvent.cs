namespace Domain.Events;

public class ParticipantBeforeInsertEvent : BaseEvent
{
    public ParticipantBeforeInsertEvent(Participant item)
    {
        Item = item;
    }

    public Participant Item { get; }
}
