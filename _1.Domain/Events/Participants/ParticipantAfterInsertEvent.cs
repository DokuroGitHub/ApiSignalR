namespace Domain.Events;

public class ParticipantAfterInsertEvent : BaseEvent
{
    public ParticipantAfterInsertEvent(Participant item)
    {
        Item = item;
    }

    public Participant Item { get; }
}
